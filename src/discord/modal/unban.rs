use super::ModJob;
use crate::{discord::Context, models::BotResult};
use twilight_model::{
    gateway::payload::incoming::InteractionCreate,
    id::{Id, marker::UserMarker},
};

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let Some(job) = ModJob::begin(ctx, interaction).await else {
        return Ok(());
    };

    // `new_checked` em vez de `Id::new(..)`, que dá panic para o valor 0.
    let targets: Vec<Id<UserMarker>> = job
        .fields
        .selected("users")
        .unwrap_or_default()
        .iter()
        .filter_map(|value| value.parse::<u64>().ok())
        .filter_map(Id::new_checked)
        .collect();

    if targets.is_empty() {
        job.fail("No valid users selected.").await;
        return Ok(());
    }

    let (mut success, mut failed) = (Vec::new(), Vec::new());
    for id in targets {
        match ctx.http.delete_ban(job.guild_id, id).await {
            Ok(_) => success.push(id),
            Err(err) => {
                tracing::error!(?err, user = %id, "falha ao desbanir usuário");
                failed.push(id);
            }
        }
    }

    job.finish("Unban", &success, &failed, "").await;
    Ok(())
}
