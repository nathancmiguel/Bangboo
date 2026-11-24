use crate::{
    discord::Context,
    functions::{global_message, system_channel_id},
    models::BotResult,
};
use twilight_gateway::EventType;
use twilight_model::gateway::payload::incoming::MemberAdd;

pub async fn handle(ctx: &Context, event: &MemberAdd) -> BotResult {
    if event.member.user.bot {
        return Ok(());
    }

    let Some(channel_id) = system_channel_id(ctx, event.guild_id).await? else {
        tracing::warn!(guild = %event.guild_id, "canal de sistema não configurado");
        return Ok(());
    };

    global_message(
        ctx,
        channel_id,
        EventType::MemberAdd,
        Some(&event.member),
        &event.member.user,
    )
    .await;
    Ok(())
}
