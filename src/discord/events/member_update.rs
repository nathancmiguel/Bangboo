use crate::{constants::GUILD, discord::Context, functions::global_message, models::BotResult};
use twilight_gateway::EventType;
use twilight_model::{
    gateway::payload::incoming::MemberUpdate,
    id::{
        Id,
        marker::{ChannelMarker, RoleMarker},
    },
};

pub async fn handle(ctx: &Context, event: &MemberUpdate) -> BotResult {
    if event.user.bot {
        return Ok(());
    }

    let booster_role = Id::<RoleMarker>::new(GUILD.roles.boosters);
    let has_role = event.roles.contains(&booster_role);
    let is_boosting = event.premium_since.is_some();

    // Antes: a cada MemberUpdate (apelido, cargo...) o bot chamava a API e, para quem impulsiona,
    // reanunciava o boost. Agora só age quando o cargo realmente precisa mudar.
    if is_boosting && !has_role {
        ctx.http
            .add_guild_member_role(event.guild_id, event.user.id, booster_role)
            .await?;
        let channel_id = Id::<ChannelMarker>::new(GUILD.channels.announcement);
        //global_boost(ctx, &event.user, channel_id).await;
        global_message(ctx, channel_id, EventType::MemberUpdate, None, &event.user).await;
    } else if !is_boosting && has_role {
        ctx.http
            .remove_guild_member_role(event.guild_id, event.user.id, booster_role)
            .await?;
    }

    Ok(())
}
