use crate::models::BotResult;

/// Configuração lida do ambiente.
pub struct Settings {
    pub bot_token: String,
    pub discloud_token: String,
}

impl Settings {
    pub fn load() -> BotResult<Self> {
        // O .env é opcional: em produção (Discloud) as variáveis já vêm do ambiente.
        match dotenvy::dotenv() {
            Ok(path) => tracing::info!("✔ .env carregado ({})", path.display()),
            Err(err) if err.not_found() => {
                tracing::warn!("⚠ .env não encontrado; usando as variáveis do ambiente")
            }
            Err(err) => return Err(format!("✘ .env inválido: {err}").into()),
        }

        let mut missing = Vec::new();
        let mut var = |name: &'static str| {
            let value = std::env::var(name).ok().filter(|value| !value.trim().is_empty());
            if value.is_none() {
                missing.push(name);
            }
            value.unwrap_or_default()
        };

        let bot_token = var("BOT_TOKEN");
        let discloud_token = var("DISCLOUD_TOKEN");
        // O `genai` lê GEMINI_API_KEY direto do ambiente; só validamos se o recurso estiver ligado.
        #[cfg(feature = "gemini")]
        var("GEMINI_API_KEY");

        if !missing.is_empty() {
            return Err(format!("✘ variáveis de ambiente ausentes: {}", missing.join(", ")).into());
        }

        tracing::info!("✔ variáveis de ambiente OK");
        Ok(Self {
            bot_token,
            discloud_token,
        })
    }
}
