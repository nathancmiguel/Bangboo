//! Cada comando é um módulo com `NAME`, `command()` (definição) e `run()` (execução).
//! Registro e despacho são só duas listas aqui: sem trait, sem `Box<dyn ...>`, sem `async_trait`.

mod age;
mod discloud;
mod fab;
mod moderate;
#[cfg(feature = "gemini")]
mod prompt;
mod social;

use crate::{discord::Context, models::BotResult};
use twilight_model::{
    application::command::Command, gateway::payload::incoming::InteractionCreate,
};

/// Definições enviadas ao Discord na inicialização.
pub fn all() -> Vec<Command> {
    #[allow(unused_mut)]
    let mut commands = vec![
        age::command(),
        discloud::command(),
        fab::command(),
        moderate::command(),
        social::command(),
    ];

    #[cfg(feature = "gemini")]
    commands.push(prompt::command());

    commands
}

pub async fn dispatch(ctx: &Context, interaction: &InteractionCreate, name: &str) -> BotResult {
    match name {
        age::NAME => age::run(ctx, interaction).await,
        discloud::NAME => discloud::run(ctx, interaction).await,
        fab::NAME => fab::run(ctx, interaction).await,
        moderate::NAME => moderate::run(ctx, interaction).await,
        social::NAME => social::run(ctx, interaction).await,
        #[cfg(feature = "gemini")]
        prompt::NAME => prompt::run(ctx, interaction).await,
        _ => {
            tracing::warn!(name, "comando sem handler");
            Ok(())
        }
    }
}
