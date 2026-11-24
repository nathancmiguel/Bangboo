use crate::{constants::GUILD, discord::Context, functions::global_message, models::BotResult};
use twilight_gateway::EventType;
use twilight_model::gateway::payload::incoming::MemberUpdate;

pub async fn handle(ctx: &Context, event: &MemberUpdate) -> BotResult {
    if event.user.bot {
        return Ok(());
    }

    let booster_role = GUILD.roles.boosters;
    let has_role = event.roles.contains(&booster_role);
    let is_boosting = event.premium_since.is_some();

    // Só age quando o cargo realmente precisa mudar (MemberUpdate dispara a cada apelido/cargo).
    if is_boosting && !has_role {
        ctx.http
            .add_guild_member_role(event.guild_id, event.user.id, booster_role)
            .await?;
        global_message(
            ctx,
            GUILD.channels.announcement,
            EventType::MemberUpdate,
            None,
            &event.user,
        )
        .await;
    } else if !is_boosting && has_role {
        ctx.http
            .remove_guild_member_role(event.guild_id, event.user.id, booster_role)
            .await?;
    }

    Ok(())
}
