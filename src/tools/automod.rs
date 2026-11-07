use crate::{constants::*, discord::Context, functions::*};
use twilight_http::request::AuditLogReason;
use twilight_model::channel::Message;
use twilight_util::builder::embed::EmbedBuilder;

const REASON: &str = "Captured by mousetrap.";
const WEEK_SECS: i64 = 60 * 60 * 24 * 7;
const HACKED_ACCOUNT_DM: &str = "Hello! Bangboo has identified that your account sent a series of suspicious messages on the server's channels. As you are a long-time member of the server, your ban has been automatically removed since your account was likely hacked. I recommend that you review your account security!";

/// Qualquer mensagem no canal-armadilha vira ban (com limpeza de 7 dias de mensagens).
/// Membros com mais de uma semana de casa são desbanidos em seguida (conta provavelmente invadida).
pub async fn mousetrap(ctx: &Context, message: &Message) {
    if message.channel_id != GUILD.channels.mousetrap {
        return;
    }
    let Some(guild_id) = message.guild_id else {
        return;
    };

    let id = message.author.id;
    let username = message.author.name.as_str();

    let long_time_member = message
        .member
        .as_ref()
        .and_then(|member| member.joined_at)
        .is_some_and(|joined_at| now_secs() - joined_at.as_secs() > WEEK_SECS);

    // O DM precisa sair antes do ban: depois dele o bot pode perder o contexto em comum.
    if long_time_member {
        notify_hacked_account(ctx, id, username).await;
    }

    // Melhor esforço: o ban abaixo também apaga mensagens recentes, então isto pode já não existir.
    if let Err(err) = ctx
        .http
        .delete_message(message.channel_id, message.id)
        .reason(REASON)
        .await
    {
        tracing::debug!(?err, username, "não foi possível apagar a mensagem do mousetrap");
    }

    if let Err(err) = ctx
        .http
        .create_ban(guild_id, id)
        .delete_message_seconds(604_800)
        .reason(REASON)
        .await
    {
        tracing::error!(?err, username, "falha ao banir usuário capturado pelo mousetrap");
        return;
    }

    if long_time_member {
        if let Err(err) = ctx.http.delete_ban(guild_id, id).await {
            tracing::error!(?err, username, "falha ao desbanir após o mousetrap");
        }
    }
}

async fn notify_hacked_account(
    ctx: &Context,
    id: twilight_model::id::Id<twilight_model::id::marker::UserMarker>,
    username: &str,
) {
    let result = async {
        let channel = ctx.http.create_private_channel(id).await?.model().await?;
        let embed = EmbedBuilder::new()
            .description(HACKED_ACCOUNT_DM)
            .color(COLORS.warning)
            .build();
        ctx.http.create_message(channel.id).embeds(&[embed]).await?;
        Ok::<(), crate::models::BotError>(())
    }
    .await;

    if let Err(err) = result {
        tracing::error!(%err, username, "falha ao enviar DM após captura pelo mousetrap");
    }
}
