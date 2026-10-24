use super::ModJob;
use crate::{discord::Context, models::BotResult};
use twilight_http::request::AuditLogReason;
use twilight_model::gateway::payload::incoming::InteractionCreate;

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let Some(job) = ModJob::begin(ctx, interaction).await else {
        return Ok(());
    };
    let Some(targets) = job.targets().await else {
        return Ok(());
    };
    let reason = job.reason();

    let (mut success, mut failed) = (Vec::new(), Vec::new());
    for id in targets {
        let result = ctx
            .http
            .create_ban(job.guild_id, id)
            .delete_message_seconds(604_800)
            .reason(reason)
            .await;

        match result {
            Ok(_) => success.push(id),
            Err(err) => {
                tracing::error!(?err, user = %id, "falha ao banir usuário");
                failed.push(id);
            }
        }
    }

    job.finish("Ban", &success, &failed, reason).await;
    Ok(())
}
