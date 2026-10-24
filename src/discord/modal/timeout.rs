use super::ModJob;
use crate::{
    discord::Context,
    functions::now_secs,
    menus::modals::timeout::DURATIONS,
    models::BotResult,
};
use twilight_http::request::AuditLogReason;
use twilight_model::{gateway::payload::incoming::InteractionCreate, util::Timestamp};

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let Some(job) = ModJob::begin(ctx, interaction).await else {
        return Ok(());
    };
    let Some(targets) = job.targets().await else {
        return Ok(());
    };
    let reason = job.reason();

    // Só aceita durações oferecidas no menu (antes: qualquer string numérica, e `values[0]`
    // dava panic se viesse vazio).
    let seconds = job
        .fields
        .selected("duration")
        .and_then(|values| values.first())
        .and_then(|value| value.parse::<u64>().ok())
        .filter(|seconds| DURATIONS.iter().any(|(_, allowed)| allowed == seconds));
    let Some(seconds) = seconds else {
        job.fail("Invalid duration.").await;
        return Ok(());
    };

    let until = match Timestamp::from_secs(now_secs() + seconds as i64) {
        Ok(timestamp) => timestamp,
        Err(err) => {
            tracing::error!(?err, "timestamp de timeout inválido");
            job.fail("Failed to compute the timeout duration.").await;
            return Ok(());
        }
    };

    let (mut success, mut failed) = (Vec::new(), Vec::new());
    for id in targets {
        match ctx
            .http
            .update_guild_member(job.guild_id, id)
            .communication_disabled_until(Some(until))
            .reason(reason)
            .await
        {
            Ok(_) => success.push(id),
            Err(err) => {
                tracing::error!(?err, user = %id, "falha ao aplicar timeout");
                failed.push(id);
            }
        }
    }

    job.finish("Timeout", &success, &failed, reason).await;
    Ok(())
}
