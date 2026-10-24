use super::ModJob;
use crate::{
    discord::Context,
    functions::now_secs,
    menus::modals::delete_message::MAX_DELETE_LIMIT,
    models::BotResult,
};
use twilight_model::gateway::payload::incoming::InteractionCreate;
use twilight_util::snowflake::Snowflake;

/// Bulk delete não aceita mensagens com mais de 14 dias (margem de 1h).
const MAX_AGE_SECS: i64 = 14 * 24 * 60 * 60 - 3600;

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let Some(job) = ModJob::begin(ctx, interaction).await else {
        return Ok(());
    };

    let Some(users) = job.fields.users("users") else {
        job.fail("Failed to read the selected users.").await;
        return Ok(());
    };
    let Some(channel_id) = interaction.channel.as_ref().map(|channel| channel.id) else {
        job.fail("Failed to read the channel.").await;
        return Ok(());
    };

    // A API só aceita 1..=100 (antes: valor inválido virava 0 e a requisição falhava).
    let limit = job
        .fields
        .text("limit")
        .and_then(|value| value.trim().parse::<u16>().ok())
        .map(|value| value.clamp(1, MAX_DELETE_LIMIT))
        .unwrap_or(1);

    let messages = match ctx.http.channel_messages(channel_id).limit(limit).await {
        Ok(response) => match response.model().await {
            Ok(messages) => messages,
            Err(err) => {
                tracing::error!(?err, "falha ao desserializar mensagens");
                job.fail("Failed to read the channel messages.").await;
                return Ok(());
            }
        },
        Err(err) => {
            tracing::error!(?err, "falha ao buscar mensagens");
            job.fail("Failed to fetch the channel messages.").await;
            return Ok(());
        }
    };

    let now_ms = now_secs() * 1000;
    let ids: Vec<_> = messages
        .iter()
        .filter(|message| users.contains(&message.author.id))
        .filter(|message| now_ms - message.id.timestamp() < MAX_AGE_SECS * 1000)
        .map(|message| message.id)
        .collect();

    // bulk delete exige de 2 a 100 mensagens; com 1 usamos o delete individual.
    let result = match ids.as_slice() {
        [] => {
            job.fail("No recent messages from the selected users were found.").await;
            return Ok(());
        }
        [single] => ctx.http.delete_message(channel_id, *single).await.map(drop),
        many => ctx.http.delete_messages(channel_id, many).await.map(drop),
    };

    let (success, failed) = match result {
        Ok(()) => (users.to_vec(), Vec::new()),
        Err(err) => {
            tracing::error!(?err, "falha ao apagar mensagens");
            (Vec::new(), users.to_vec())
        }
    };

    job.finish("Delete messages", &success, &failed, "").await;
    Ok(())
}
