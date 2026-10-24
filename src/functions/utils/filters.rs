use crate::{
    constants::GUILD,
    discord::Context,
    models::BotResult,
};
use twilight_model::{
    gateway::payload::incoming::InteractionCreate,
    id::{
        Id,
        marker::{GuildMarker, UserMarker},
    },
};

/// Quem executou a interação é dono/moderador?
pub fn invoker_is_staff(interaction: &InteractionCreate) -> bool {
    interaction
        .member
        .as_ref()
        .is_some_and(|member| GUILD.is_staff(&member.roles))
}

/// Remove da lista os usuários que são dono/moderadores (eles não podem ser alvo de ações de moderação).
pub async fn filter_users_from_mod_action(
    ctx: &Context,
    guild_id: Id<GuildMarker>,
    user_ids: &[Id<UserMarker>],
) -> BotResult<Vec<Id<UserMarker>>> {
    let mut filtered = Vec::with_capacity(user_ids.len());

    for &id in user_ids {
        // `map` solta o guard do cache imediatamente (nada de segurar lock durante `.await`).
        let cached = ctx
            .cache
            .member(guild_id, id)
            .map(|member| GUILD.is_staff(member.roles()));

        let is_staff = match cached {
            Some(is_staff) => is_staff,
            None => {
                let member = ctx.http.guild_member(guild_id, id).await?.model().await?;
                GUILD.is_staff(&member.roles)
            }
        };

        if !is_staff {
            filtered.push(id);
        }
    }

    Ok(filtered)
}
