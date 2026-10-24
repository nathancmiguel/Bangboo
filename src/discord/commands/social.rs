use crate::{discord::Context, functions::reply_v2, menus::components::social_component, models::BotResult};
use twilight_model::{
    application::{
        command::{Command, CommandType},
        interaction::InteractionContextType,
    },
    gateway::payload::incoming::InteractionCreate,
};
use twilight_util::builder::command::CommandBuilder;

pub const NAME: &str = "social";

pub fn command() -> Command {
    CommandBuilder::new(NAME, "Social media", CommandType::ChatInput)
        .contexts([InteractionContextType::Guild])
        .build()
}

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    reply_v2(ctx, interaction, social_component()).await;
    Ok(())
}
