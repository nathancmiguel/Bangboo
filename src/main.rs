mod constants;
mod discord;
mod env;
mod functions;
mod menus;
mod models;
mod tools;

use crate::discord::App;
use crate::env::Settings;
use crate::models::BotResult;
use twilight_model::gateway::Intents;

/// Apenas o necessário. `Intents::all()` pedia intents privilegiadas inutilizadas
/// (presenças, conteúdo de mensagem...) e fazia o gateway enviar muito mais eventos.
///
/// - GUILDS: cache de servidores
/// - GUILD_MEMBERS (privilegiada): entrada/saída/atualização de membros
/// - GUILD_MODERATION: evento de ban
/// - GUILD_MESSAGES: `mousetrap` só precisa de autor/canal (não de `MESSAGE_CONTENT`)
const INTENTS: Intents = Intents::GUILDS
    .union(Intents::GUILD_MEMBERS)
    .union(Intents::GUILD_MODERATION)
    .union(Intents::GUILD_MESSAGES);

#[tokio::main]
async fn main() {
    #[cfg(target_env = "gnu")]
    functions::configure_malloc();

    functions::init_tracing();

    if let Err(err) = run().await {
        tracing::error!(%err, "o bot encerrou com erro");
        std::process::exit(1);
    }
}

async fn run() -> BotResult {
    let settings = Settings::load()?;
    // Falha cedo se algum JSON embutido estiver inválido (em vez de dar panic no primeiro uso).
    constants::validate()?;

    let app = App::bootstrap(settings, INTENTS).await?;
    app.run().await
}
