//! Roteamento de eventos e interações.
//!
//! Substitui os 5 traits `#[async_trait]` + `Box<dyn ...>` + `HashMap` global (`HANDLERS`).
//! Todos os handlers são conhecidos em tempo de compilação, então um `match` simples basta:
//! sem alocação por handler, sem `dyn`, sem `async_trait`, e o compilador avisa se algo
//! deixar de ser usado (foi assim que o modal de unban passou despercebido: ele existia, mas
//! nunca foi adicionado à lista de handlers).

use crate::{
    discord::{Context, commands, events, message_component, modal},
    models::BotResult,
};
use twilight_gateway::Event;
use twilight_model::{
    application::interaction::InteractionData, gateway::payload::incoming::InteractionCreate,
};

/// Eventos que têm handler. O resto só passa pelo cache.
pub fn is_handled(event: &Event) -> bool {
    matches!(
        event,
        Event::Ready(_)
            | Event::BanAdd(_)
            | Event::MemberAdd(_)
            | Event::MemberRemove(_)
            | Event::MemberUpdate(_)
            | Event::MessageCreate(_)
            | Event::InteractionCreate(_)
    )
}

pub async fn handle(ctx: Context, event: Event) {
    let result = match &event {
        Event::Ready(e) => events::ready::handle(&ctx, e).await,
        Event::BanAdd(e) => events::ban_add::handle(&ctx, e).await,
        Event::MemberAdd(e) => events::member_add::handle(&ctx, e).await,
        Event::MemberRemove(e) => events::member_remove::handle(&ctx, e).await,
        Event::MemberUpdate(e) => events::member_update::handle(&ctx, e).await,
        Event::MessageCreate(e) => events::message_create::handle(&ctx, e).await,
        Event::InteractionCreate(e) => handle_interaction(&ctx, e).await,
        _ => Ok(()),
    };

    if let Err(err) = result {
        tracing::error!(event = ?event.kind(), %err, "falha ao tratar evento");
    }
}

async fn handle_interaction(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    match interaction.data.as_ref() {
        Some(InteractionData::ApplicationCommand(command)) => {
            commands::dispatch(ctx, interaction, &command.name).await
        }
        Some(InteractionData::ModalSubmit(modal)) => {
            modal::dispatch(ctx, interaction, &modal.custom_id).await
        }
        Some(InteractionData::MessageComponent(component)) => {
            message_component::dispatch(ctx, interaction, &component.custom_id).await
        }
        _ => Ok(()),
    }
}
