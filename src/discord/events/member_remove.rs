use crate::{
    discord::Context,
    functions::{global_message, system_channel_id},
    models::BotResult,
};
use twilight_gateway::EventType;
use twilight_model::gateway::payload::incoming::MemberRemove;

pub async fn handle(ctx: &Context, event: &MemberRemove) -> BotResult {
    if event.user.bot {
        return Ok(());
    }

    // `http.ban` é o GET do ban: se existir, a saída foi um banimento (já anunciado por `BanAdd`).
    if ctx.http.ban(event.guild_id, event.user.id).await.is_ok() {
        return Ok(());
    }

    let Some(channel_id) = system_channel_id(ctx, event.guild_id).await? else {
        tracing::warn!(guild = %event.guild_id, "canal de sistema não configurado");
        return Ok(());
    };

    global_message(ctx, channel_id, EventType::MemberRemove, None, &event.user).await;
    Ok(())
}
