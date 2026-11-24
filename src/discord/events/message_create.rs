use crate::{discord::Context, models::BotResult, tools::mousetrap};
use twilight_model::gateway::payload::incoming::MessageCreate;

pub async fn handle(ctx: &Context, message: &MessageCreate) -> BotResult {
    // Bug corrigido: o filtro antigo olhava `message.member.user`, que o Discord não envia em
    // MESSAGE_CREATE (sempre `None`), então bots nunca eram ignorados (nem o próprio Bangboo).
    if message.author.bot {
        return Ok(());
    }

    mousetrap(ctx, message).await;
    Ok(())
}
