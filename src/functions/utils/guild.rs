use crate::{discord::Context, models::BotResult};
use twilight_model::id::{
    Id,
    marker::{ChannelMarker, GuildMarker},
};

/// Canal de sistema do servidor (cache primeiro, HTTP como fallback).
/// Esse trecho estava copiado em ban_add, member_add e member_remove.
pub async fn system_channel_id(
    ctx: &Context,
    guild_id: Id<GuildMarker>,
) -> BotResult<Option<Id<ChannelMarker>>> {
    // O guard do cache é descartado aqui, antes de qualquer `.await`.
    let cached = ctx
        .cache
        .guild(guild_id)
        .and_then(|guild| guild.system_channel_id());
    if cached.is_some() {
        return Ok(cached);
    }

    let guild = ctx.http.guild(guild_id).await?.model().await?;
    Ok(guild.system_channel_id)
}
