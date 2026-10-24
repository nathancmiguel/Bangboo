mod ban;
mod delete_message;
mod kick;
mod timeout;
mod unban;

use crate::{
    constants::{COLORS, ids},
    discord::Context,
    functions::{
        ModalFields, defer_reply, filters::filter_users_from_mod_action,
        filters::invoker_is_staff, get_modal_data, officer_cui_action_embed, update_reply,
        update_with_embed,
    },
    models::BotResult,
};
use twilight_model::{
    channel::message::MessageFlags,
    gateway::payload::incoming::InteractionCreate,
    id::{
        Id,
        marker::{GuildMarker, UserMarker},
    },
    user::User,
};
use twilight_util::builder::InteractionResponseDataBuilder;

/// Todos os modais de moderação estavam aqui, um por trait impl. Agora é um `match`;
/// o `unban`, que antes ficou de fora da lista de handlers, está registrado.
pub async fn dispatch(ctx: &Context, interaction: &InteractionCreate, custom_id: &str) -> BotResult {
    match custom_id {
        ids::MODAL_BAN => ban::run(ctx, interaction).await,
        ids::MODAL_KICK => kick::run(ctx, interaction).await,
        ids::MODAL_TIMEOUT => timeout::run(ctx, interaction).await,
        ids::MODAL_UNBAN => unban::run(ctx, interaction).await,
        ids::MODAL_DELETE_MESSAGE => delete_message::run(ctx, interaction).await,
        _ => {
            tracing::warn!(custom_id, "modal sem handler");
            Ok(())
        }
    }
}

/// Preâmbulo/epílogo comum das ações de moderação (cada uma repetia ~60 linhas disso).
struct ModJob<'a> {
    ctx: &'a Context,
    interaction: &'a InteractionCreate,
    guild_id: Id<GuildMarker>,
    moderator: &'a User,
    fields: ModalFields<'a>,
}

impl<'a> ModJob<'a> {
    /// Adia a resposta, confere a permissão e lê os campos. `None` = o usuário já foi avisado.
    /// Toda saída depois do `defer` responde ao usuário; antes, um campo inválido deixava a
    /// interação eternamente em "pensando...".
    async fn begin(ctx: &'a Context, interaction: &'a InteractionCreate) -> Option<Self> {
        defer_reply(ctx, interaction, true).await;

        // Defesa em profundidade: o modal só abre para staff, mas o submit é revalidado.
        if !invoker_is_staff(interaction) {
            fail(ctx, interaction, "You are not a mod or the owner of the guild.").await;
            return None;
        }

        let (Some(data), Some(guild_id), Some(moderator)) = (
            get_modal_data(interaction),
            interaction.guild_id,
            interaction.author(),
        ) else {
            fail(ctx, interaction, "Failed to read the form data.").await;
            return None;
        };

        Some(Self {
            ctx,
            interaction,
            guild_id,
            moderator,
            fields: ModalFields::new(&data.components),
        })
    }

    fn reason(&self) -> &'a str {
        self.fields.text("reason").unwrap_or_default()
    }

    /// Usuários escolhidos, já sem dono/moderadores.
    async fn targets(&self) -> Option<Vec<Id<UserMarker>>> {
        let Some(selected) = self.fields.users("users") else {
            fail(self.ctx, self.interaction, "Failed to read the selected users.").await;
            return None;
        };

        match filter_users_from_mod_action(self.ctx, self.guild_id, selected).await {
            Ok(users) if users.is_empty() => {
                fail(self.ctx, self.interaction, "No valid users selected.").await;
                None
            }
            Ok(users) => Some(users),
            Err(err) => {
                tracing::error!(%err, "falha ao filtrar os alvos da moderação");
                fail(self.ctx, self.interaction, "Failed to check the selected users.").await;
                None
            }
        }
    }

    async fn fail(&self, message: &str) {
        fail(self.ctx, self.interaction, message).await;
    }

    async fn finish(
        &self,
        action: &str,
        success: &[Id<UserMarker>],
        failed: &[Id<UserMarker>],
        reason: &str,
    ) {
        let embed = officer_cui_action_embed(self.moderator, action, success, failed, reason);
        let payload = InteractionResponseDataBuilder::new()
            .flags(MessageFlags::EPHEMERAL)
            .embeds(vec![embed])
            .build();

        update_reply(self.ctx, self.interaction, payload).await;
    }
}

async fn fail(ctx: &Context, interaction: &InteractionCreate, message: &str) {
    update_with_embed(ctx, interaction, MessageFlags::EPHEMERAL, COLORS.danger, message).await;
}
