use crate::{
    constants::ids,
    discord::Context,
    functions::defer_update_reply,
    models::BotResult,
    tools::discloud::{update_logs_panel, update_status_panel},
};
use twilight_model::gateway::payload::incoming::InteractionCreate;

pub async fn dispatch(ctx: &Context, interaction: &InteractionCreate, custom_id: &str) -> BotResult {
    match custom_id {
        ids::DISCLOUD_STATUS_REFRESH => {
            defer_update_reply(ctx, interaction).await;
            update_status_panel(ctx, interaction).await;
        }
        ids::DISCLOUD_LOGS_REFRESH => {
            defer_update_reply(ctx, interaction).await;
            update_logs_panel(ctx, interaction).await;
        }
        _ => tracing::warn!(custom_id, "componente sem handler"),
    }
    Ok(())
}
