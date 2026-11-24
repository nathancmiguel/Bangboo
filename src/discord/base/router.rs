//! Roteamento de eventos e interações.
//!
//! Todos os handlers são conhecidos em tempo de compilação, então um `match` basta: sem
//! alocação por handler, sem `dyn`, e o compilador avisa se algo deixar de ser usado.
//! Ao adicionar um evento/modal/componente, inclua-o também em `NAMES`/`IDS` do módulo.

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
        tracing::error!(event = ?event.kind(), %err, "✘ falha ao tratar evento");
    }
}

/// Erros de interação são logados aqui, com o nome do comando/`custom_id` que falhou.
async fn handle_interaction(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let (kind, id, result) = match interaction.data.as_ref() {
        Some(InteractionData::ApplicationCommand(command)) => (
            "comando",
            command.name.as_str(),
            commands::dispatch(ctx, interaction, &command.name).await,
        ),
        Some(InteractionData::ModalSubmit(modal)) => (
            "modal",
            modal.custom_id.as_str(),
            modal::dispatch(ctx, interaction, &modal.custom_id).await,
        ),
        Some(InteractionData::MessageComponent(component)) => (
            "componente",
            component.custom_id.as_str(),
            message_component::dispatch(ctx, interaction, &component.custom_id).await,
        ),
        _ => return Ok(()),
    };

    if let Err(err) = result {
        tracing::error!(%err, "✘ falha no {kind} `{id}`");
    }
    Ok(())
}
