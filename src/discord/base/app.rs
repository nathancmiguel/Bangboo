use crate::{
    discord::{base::router, commands},
    env::Settings,
    models::BotResult,
};
use discloud_rs::Discloud;
use std::{ops::Deref, sync::Arc, time::Duration};
use tokio::{
    task::JoinSet,
    time::{Instant, sleep, timeout},
};
use twilight_cache_inmemory::{DefaultInMemoryCache, InMemoryCache, ResourceType};
use twilight_gateway::{
    CloseFrame, Config, EventTypeFlags, Intents, MessageSender, Shard, StreamExt as _,
};
use twilight_http::Client;

/// Com que frequência perguntar ao Discord se o número recomendado de shards mudou.
const RESHARD_CHECK_INTERVAL: Duration = Duration::from_secs(60 * 60 * 8);
/// Quanto esperar as shards fecharem a conexão antes de abortar as tasks.
const SHUTDOWN_GRACE: Duration = Duration::from_secs(5);

/// Só os eventos que o bot trata ou que o cache precisa. O twilight não desserializa
/// os demais, o que economiza CPU/memória (antes: `EventTypeFlags::all()`).
const EVENT_FLAGS: EventTypeFlags = EventTypeFlags::READY
    .union(EventTypeFlags::GUILD_CREATE)
    .union(EventTypeFlags::GUILD_UPDATE)
    .union(EventTypeFlags::GUILD_DELETE)
    .union(EventTypeFlags::MEMBER_ADD)
    .union(EventTypeFlags::MEMBER_REMOVE)
    .union(EventTypeFlags::MEMBER_UPDATE)
    .union(EventTypeFlags::MEMBER_CHUNK)
    .union(EventTypeFlags::BAN_ADD)
    .union(EventTypeFlags::MESSAGE_CREATE)
    .union(EventTypeFlags::INTERACTION_CREATE);

/// Estado compartilhado por todas as shards e handlers.
pub struct Shared {
    pub http: Client,
    pub cache: InMemoryCache,
    /// Cliente HTTP genérico (avatares, etc.). Reutilizar o cliente reaproveita conexões
    /// (antes: `reqwest::get` criava um cliente novo a cada chamada, sem timeout).
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
        // `Client::new` já vem com o rate limiter padrão: o `RateLimiter::default()` passado
        // manualmente (e a crate `twilight-http-ratelimiting`) era redundante.
        let http = Client::new(settings.bot_token.clone());

        // Só o que o bot consulta: servidores (canal de sistema) e membros (cargos).
        let cache = DefaultInMemoryCache::builder()
            .resource_types(ResourceType::GUILD | ResourceType::MEMBER)
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

        app.register_commands().await?;
        Ok(app)
    }

    /// Registra os slash commands **uma vez, na inicialização**.
    /// Antes isso ficava no evento `Ready`, que dispara a cada shard, a cada reconexão com
    /// nova sessão e a cada reshard, sobrescrevendo os comandos globais toda vez.
    async fn register_commands(&self) -> BotResult {
        let application = self
            .shared
            .http
            .current_user_application()
            .await?
            .model()
            .await?;

        let commands = commands::all();
        self.shared
            .http
            .interaction(application.id)
            .set_global_commands(&commands)
            .await?;

        for command in &commands {
            tracing::info!(command = %command.name, "slash command registrado");
        }
        Ok(())
    }

    async fn create_shards(&self) -> BotResult<Vec<Shard>> {
        let shards = twilight_gateway::create_recommended(
            &self.shared.http,
            self.gateway.clone(),
            |_, builder| builder.build(),
        )
        .await?;

        Ok(shards.collect())
    }

    pub async fn run(self) -> BotResult {
        let mut shards = self.create_shards().await?;

        let shutdown = shutdown_signal();
        tokio::pin!(shutdown);

        loop {
            let senders: Vec<MessageSender> = shards.iter().map(Shard::sender).collect();

            let mut tasks = JoinSet::new();
            for shard in shards.drain(..) {
                tasks.spawn(shard_loop(self.shared.clone(), shard));
            }
            tracing::info!(shards = senders.len(), "shards iniciadas");

            let reshard = sleep(RESHARD_CHECK_INTERVAL);
            tokio::pin!(reshard);

            let next = loop {
                tokio::select! {
                    _ = &mut shutdown => break Next::Shutdown,

                    _ = &mut reshard => {
                        // Só refaz as shards se o Discord recomendar outra quantidade.
                        // (Antes: derrubava e recriava as shards a cada 8h, mesmo sem necessidade.)
                        match self.create_shards().await {
                            Ok(fresh) if fresh.len() != senders.len() => {
                                tracing::info!(antes = senders.len(), depois = fresh.len(), "resharding");
                                shards = fresh;
                                break Next::Reshard;
                            }
                            Ok(_) => {}
                            Err(err) => tracing::error!(%err, "falha ao consultar o número recomendado de shards"),
                        }
                        reshard.as_mut().reset(Instant::now() + RESHARD_CHECK_INTERVAL);
                    }

                    finished = tasks.join_next() => match finished {
                        Some(Ok(())) => tracing::warn!("uma shard encerrou"),
                        Some(Err(err)) => tracing::error!(?err, "task de shard falhou (panic/cancelada)"),
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
    let ctx = Context {
        shared,
        sender: shard.sender(),
    };

    while let Some(item) = shard.next_event(EVENT_FLAGS).await {
        let event = match item {
            Ok(event) => event,
            Err(source) => {
                tracing::warn!(?source, "erro ao receber evento do gateway");
                continue;
            }
        };

        ctx.cache.update(&event);

        // Não cria uma task para eventos que o bot só usa no cache.
        if router::is_handled(&event) {
            tokio::spawn(router::handle(ctx.clone(), event));
        }
    }
}
