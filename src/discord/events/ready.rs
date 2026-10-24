use crate::{discord::Context, models::BotResult};
use twilight_model::gateway::{
    payload::{incoming::Ready, outgoing::UpdatePresence},
    presence::{Activity, ActivityType, Status},
};

pub async fn handle(ctx: &Context, ready: &Ready) -> BotResult {
    tracing::info!(user = %ready.user.name, "online");

    let activity = Activity {
        application_id: None,
        assets: None,
        buttons: vec![],
        created_at: None,
        details: None,
        emoji: None,
        flags: None,
        id: None,
        instance: None,
        kind: ActivityType::Custom,
        name: "Custom Status".to_string(),
        party: None,
        secrets: None,
        state: Some("Rust-powered bot.\nHosted by discloud.".to_string()),
        timestamps: None,
        url: None,
    };

    match UpdatePresence::new(vec![activity], false, None, Status::Online) {
        Ok(presence) => ctx.sender.command(&presence)?,
        Err(err) => tracing::warn!(?err, "presença inválida"),
    }

    Ok(())
}
