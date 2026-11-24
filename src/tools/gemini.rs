use crate::models::BotResult;
use genai::{
    Client,
    chat::{ChatMessage, ChatRequest},
};
use std::sync::LazyLock;

const MODEL_GEMINI: &str = "gemini-3-flash-preview";

static GEMINI: LazyLock<Client> =
    LazyLock::new(|| Client::new().expect("falha ao criar o cliente do Gemini"));

pub async fn get_text(prompt: &str) -> BotResult<String> {
    GEMINI.resolve_service_target(MODEL_GEMINI).await?;

    let chat_req = ChatRequest::new(vec![ChatMessage::user(prompt)]);
    let chat_res = GEMINI.exec_chat(MODEL_GEMINI, chat_req, None).await?;

    Ok(chat_res.into_texts().join("\n"))
}
