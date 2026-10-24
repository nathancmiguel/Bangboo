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
        match ctx
            .http
            .remove_guild_member(job.guild_id, id)
            .reason(reason)
            .await
        {
            Ok(_) => success.push(id),
            Err(err) => {
                tracing::error!(?err, user = %id, "falha ao expulsar usuário");
                failed.push(id);
            }
        }
    }

    job.finish("Kick", &success, &failed, reason).await;
    Ok(())
}
