use crate::{
    discord::{base::router, commands, events, message_component, modal},
    env::Settings,
    models::{BotError, BotResult},
};
use discloud_rs::Discloud;
use std::{future::Future, ops::Deref, sync::Arc, time::Duration};
use tokio::{
    task::JoinSet,
    time::{Instant, sleep, timeout},
};
use twilight_cache_inmemory::{DefaultInMemoryCache, InMemoryCache, ResourceType};
use twilight_gateway::{
    CloseFrame, Config, Event, EventTypeFlags, Intents, MessageSender, Shard, ShardId,
    StreamExt as _, error::ReceiveMessageErrorType,
};
use twilight_http::{Client, error::ErrorType};

/// Com que frequência perguntar ao Discord se o número recomendado de shards mudou.
const RESHARD_CHECK_INTERVAL: Duration = Duration::from_secs(60 * 60 * 8);
/// Quanto esperar as shards fecharem a conexão antes de abortar as tasks.
const SHUTDOWN_GRACE: Duration = Duration::from_secs(5);
/// Tentativas das chamadas de inicialização (a API do Discord pode estar fora do ar).
const STARTUP_ATTEMPTS: u32 = 5;
const STARTUP_BACKOFF: Duration = Duration::from_secs(2);
const INVALID_TOKEN: &str = "BOT_TOKEN inválido, expirado ou revogado";

/// Só os eventos que o bot trata ou que o cache precisa. O twilight não desserializa
/// os demais, o que economiza CPU/memória. `GatewayClose` é sempre entregue.
const EVENT_FLAGS: EventTypeFlags = EventTypeFlags::READY
    .union(EventTypeFlags::RESUMED)
    .union(EventTypeFlags::GUILD_CREATE)
    .union(EventTypeFlags::GUILD_UPDATE)
    .union(EventTypeFlags::GUILD_DELETE)
    .union(EventTypeFlags::MEMBER_ADD)
    .union(EventTypeFlags::MEMBER_REMOVE)
    .union(EventTypeFlags::MEMBER_UPDATE)
    .union(EventTypeFlags::BAN_ADD)
    .union(EventTypeFlags::MESSAGE_CREATE)
    .union(EventTypeFlags::INTERACTION_CREATE);

/// Estado compartilhado por todas as shards e handlers.
pub struct Shared {
    pub http: Client,
    pub cache: InMemoryCache,
    /// Cliente HTTP genérico (avatares, etc.), reutilizado para aproveitar conexões.
    pub web: reqwest::Client,
    pub discloud: Discloud,
}

/// Contexto entregue aos handlers. Clonar é barato (um `Arc` + o sender da shard) e os campos
/// de `Shared` continuam acessíveis como `ctx.http`, `ctx.cache`... via `Deref`.
#[derive(Clone)]
pub struct Context {
    shared: Arc<Shared>,
    pub sender: MessageSender,
}

impl Deref for Context {
    type Target = Shared;

    fn deref(&self) -> &Shared {
        &self.shared
    }
}

pub struct App {
    shared: Arc<Shared>,
    gateway: Config,
}

enum Next {
    Shutdown,
    Reshard,
    AllStopped,
}

impl App {
    pub async fn bootstrap(settings: Settings, intents: Intents) -> BotResult<Self> {
        let http = Client::new(settings.bot_token.clone());

        // Só servidores (canal de sistema). Membros não ficam em cache: o cache crescia junto com
        // o servidor, e os cargos dos alvos de moderação já vêm na própria interação.
        let cache = DefaultInMemoryCache::builder()
            .resource_types(ResourceType::GUILD)
            .build();

        let web = reqwest::Client::builder()
            .timeout(Duration::from_secs(10))
            .build()?;

        let app = Self {
            shared: Arc::new(Shared {
                http,
                cache,
                web,
                discloud: Discloud::new(&settings.discloud_token),
            }),
            gateway: Config::new(settings.bot_token, intents),
        };

        log_registered("eventos", events::NAMES.iter().copied());
        log_registered(
            "responders",
            modal::IDS.iter().chain(message_component::IDS).copied(),
        );
        app.register_commands().await?;
        Ok(app)
    }

    /// Registra os slash commands **uma vez, na inicialização** (o `Ready` dispara a cada
    /// reconexão/reshard). Se a API estiver fora, o bot segue com os comandos já registrados;
    /// só um token inválido interrompe a inicialização.
    async fn register_commands(&self) -> BotResult {
        let commands = commands::all();
        let http = &self.shared.http;

        let result = retry("registrar os slash commands", || async {
            let application = http.current_user_application().await?.model().await?;
            http.interaction(application.id)
                .set_global_commands(&commands)
                .await?;
            Ok(())
        })
        .await;

        match result {
            Ok(()) => log_registered("comandos", commands.iter().map(|c| c.name.as_str())),
            Err(err) if is_unauthorized(&err) => return Err(INVALID_TOKEN.into()),
            Err(err) => tracing::error!(
                %err,
                "✘ comandos não registrados; o Discord continua usando os da última inicialização"
            ),
        }
        Ok(())
    }

    async fn create_shards(&self) -> BotResult<Vec<Shard>> {
        let info = self.shared.http.gateway().authed().await?.model().await?;
        let total = info.shards;

        Ok((0..total)
            .map(|id| Shard::with_config(ShardId::new(id, total), self.gateway.clone()))
            .collect())
    }

    pub async fn run(self) -> BotResult {
        let mut shards = retry("conectar ao gateway", || self.create_shards())
            .await
            .map_err(|err| -> BotError {
                if is_unauthorized(&err) {
                    INVALID_TOKEN.into()
                } else {
                    format!("não foi possível conectar ao gateway: {err}").into()
                }
            })?;

        let shutdown = shutdown_signal();
        tokio::pin!(shutdown);

        loop {
            let senders: Vec<MessageSender> = shards.iter().map(Shard::sender).collect();

            let mut tasks = JoinSet::new();
            for shard in shards.drain(..) {
                tasks.spawn(shard_loop(self.shared.clone(), shard));
            }
            tracing::info!(shards = senders.len(), "✔ shards iniciadas");

            let reshard = sleep(RESHARD_CHECK_INTERVAL);
            tokio::pin!(reshard);

            let next = loop {
                tokio::select! {
                    _ = &mut shutdown => break Next::Shutdown,

                    _ = &mut reshard => {
                        // Só refaz as shards se o Discord recomendar outra quantidade.
                        match self.create_shards().await {
                            Ok(fresh) if fresh.len() != senders.len() => {
                                tracing::info!(antes = senders.len(), depois = fresh.len(), "resharding");
                                shards = fresh;
                                break Next::Reshard;
                            }
                            Ok(_) => {}
                            Err(err) => tracing::warn!(%err, "falha ao consultar o número recomendado de shards"),
                        }
                        reshard.as_mut().reset(Instant::now() + RESHARD_CHECK_INTERVAL);
                    }

                    finished = tasks.join_next() => match finished {
                        Some(Ok(())) => tracing::warn!("uma shard encerrou"),
                        Some(Err(err)) => tracing::error!(%err, "task de shard falhou (panic/cancelada)"),
                        None => break Next::AllStopped,
                    },
                }
            };

            match next {
                Next::Shutdown => {
                    tracing::info!("encerrando...");
                    stop_shards(&senders, tasks).await;
                    return Ok(());
                }
                Next::Reshard => stop_shards(&senders, tasks).await,
                // Todas as shards morreram sozinhas (token/intents inválidos, por exemplo).
                // Recriar em loop só geraria uma enxurrada de chamadas ao Discord.
                Next::AllStopped => {
                    return Err("todas as shards encerraram (token ou intents inválidos?)".into());
                }
            }
        }
    }
}

fn log_registered<'a>(kind: &str, names: impl Iterator<Item = &'a str>) {
    let names: Vec<&str> = names.collect();
    tracing::info!("✔ {} {kind}: {}", names.len(), names.join(", "));
}

/// Repete `op` com backoff exponencial (2s, 4s, 8s...). Usado só na inicialização, onde uma
/// instabilidade passageira da API do Discord derrubaria o bot inteiro.
async fn retry<T, F, Fut>(what: &str, mut op: F) -> BotResult<T>
where
    F: FnMut() -> Fut,
    Fut: Future<Output = BotResult<T>>,
{
    let mut delay = STARTUP_BACKOFF;
    let mut attempt = 1;
    loop {
        match op().await {
            Ok(value) => return Ok(value),
            Err(err) if is_unauthorized(&err) => return Err(err),
            Err(err) if attempt < STARTUP_ATTEMPTS => {
                tracing::warn!(
                    %err,
                    "falha ao {what} (tentativa {attempt}/{STARTUP_ATTEMPTS}); tentando de novo em {}s",
                    delay.as_secs()
                );
                sleep(delay).await;
                delay *= 2;
                attempt += 1;
            }
            Err(err) => return Err(err),
        }
    }
}

/// Token inválido não se resolve tentando de novo (ao contrário de 5xx/timeout).
fn is_unauthorized(err: &BotError) -> bool {
    err.downcast_ref::<twilight_http::Error>()
        .is_some_and(|err| match err.kind() {
            ErrorType::Unauthorized => true,
            ErrorType::Response { status, .. } => status.get() == 401,
            _ => false,
        })
}

/// Resolve quando o processo recebe Ctrl+C ou SIGTERM (é o que a hospedagem envia ao parar o app).
async fn shutdown_signal() {
    #[cfg(unix)]
    {
        use tokio::signal::unix::{SignalKind, signal};

        if let Ok(mut terminate) = signal(SignalKind::terminate()) {
            tokio::select! {
                _ = tokio::signal::ctrl_c() => {}
                _ = terminate.recv() => {}
            }
            return;
        }
    }

    let _ = tokio::signal::ctrl_c().await;
}

/// Fecha as conexões com o gateway de forma limpa e espera as tasks terminarem.
async fn stop_shards(senders: &[MessageSender], mut tasks: JoinSet<()>) {
    for sender in senders {
        let _ = sender.close(CloseFrame::NORMAL);
    }

    let drain = async { while tasks.join_next().await.is_some() {} };
    if timeout(SHUTDOWN_GRACE, drain).await.is_err() {
        tasks.abort_all();
    }
}

async fn shard_loop(shared: Arc<Shared>, mut shard: Shard) {
    let shard_id = shard.id().number();
    let ctx = Context {
        shared,
        sender: shard.sender(),
    };

    while let Some(item) = shard.next_event(EVENT_FLAGS).await {
        let event = match item {
            Ok(event) => event,
            // `Deserializing` carrega o payload inteiro do evento: não vale a pena despejar no log.
            Err(err) => {
                if let ReceiveMessageErrorType::Deserializing { .. } = err.kind() {
                    tracing::warn!(shard = shard_id, "evento do gateway ignorado (formato desconhecido)");
                } else {
                    tracing::warn!(shard = shard_id, %err, "erro no gateway; reconectando automaticamente");
                }
                continue;
            }
        };

        match &event {
            Event::GatewayClose(frame) => {
                let code = frame.as_ref().map(|frame| frame.code);
                tracing::warn!(shard = shard_id, ?code, "⚠ desconectado do Discord, reconectando...");
                continue;
            }
            Event::Resumed => {
                tracing::info!(shard = shard_id, "✔ conexão com o Discord retomada");
                continue;
            }
            _ => {}
        }

        ctx.cache.update(&event);

        // Não cria uma task para eventos que o bot só usa no cache.
        if router::is_handled(&event) {
            tokio::spawn(router::handle(ctx.clone(), event));
        }
    }
}
