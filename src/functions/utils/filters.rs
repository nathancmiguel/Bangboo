use crate::{constants::GUILD, discord::Context, models::BotResult};
use twilight_http::error::ErrorType;
use twilight_model::{
    application::interaction::InteractionDataResolved,
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
///
/// Os cargos vêm de `resolved.members`, que o Discord envia junto com o `UserSelect`; o HTTP só é
/// usado se algum membro não vier ali. Assim o bot não precisa manter um cache de membros na memória.
pub async fn filter_users_from_mod_action(
    ctx: &Context,
    guild_id: Id<GuildMarker>,
    user_ids: &[Id<UserMarker>],
    resolved: Option<&InteractionDataResolved>,
) -> BotResult<Vec<Id<UserMarker>>> {
    let mut filtered = Vec::with_capacity(user_ids.len());

    for &id in user_ids {
        let resolved_member = resolved.and_then(|resolved| resolved.members.get(&id));

        let is_staff = match resolved_member {
            Some(member) => GUILD.is_staff(&member.roles),
            None => match ctx.http.guild_member(guild_id, id).await {
                Ok(response) => GUILD.is_staff(&response.model().await?.roles),
                // 404 = não é (mais) membro do servidor, logo não é staff. Qualquer outro erro
                // (API fora do ar...) aborta a ação: na dúvida, não arriscamos punir um moderador.
                Err(err) if is_not_found(&err) => false,
                Err(err) => return Err(err.into()),
            },
        };

        if !is_staff {
            filtered.push(id);
        }
    }

    Ok(filtered)
}

fn is_not_found(err: &twilight_http::Error) -> bool {
    matches!(err.kind(), ErrorType::Response { status, .. } if status.get() == 404)
}
