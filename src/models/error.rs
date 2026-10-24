/// Erro genérico do bot. Ele só é logado (nunca inspecionado), então `Box<dyn Error>` basta.
pub type BotError = Box<dyn std::error::Error + Send + Sync>;
pub type BotResult<T = ()> = Result<T, BotError>;
