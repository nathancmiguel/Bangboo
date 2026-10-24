use crate::models::{BotError, BotResult};

/// Configuração lida do ambiente. Substitui o `Lazy<EnvSchema>` global
/// (que dava `panic!` em qualquer ponto do programa no primeiro acesso).
pub struct Settings {
    pub bot_token: String,
    pub discloud_token: String,
}

impl Settings {
    pub fn load() -> BotResult<Self> {
        // O .env é opcional: em produção (Discloud) as variáveis já vêm do ambiente.
        if dotenvy::dotenv().is_err() {
            tracing::debug!("arquivo .env não encontrado, usando apenas o ambiente");
        }

        // O `genai` lê GEMINI_API_KEY direto do ambiente; só validamos se o recurso estiver ligado.
        #[cfg(feature = "gemini")]
        var("GEMINI_API_KEY")?;

        Ok(Self {
            bot_token: var("BOT_TOKEN")?,
            discloud_token: var("DISCLOUD_TOKEN")?,
        })
    }
}

fn var(name: &str) -> BotResult<String> {
    std::env::var(name).map_err(|_| -> BotError { format!("variável de ambiente `{name}` ausente").into() })
}
