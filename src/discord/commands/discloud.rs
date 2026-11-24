use crate::{
    constants::COLORS,
    discord::Context,
    functions::{defer_reply, filters::invoker_is_staff, get_app_command_data, reply_with_embed},
    models::BotResult,
    tools::discloud::{update_logs_panel, update_status_panel},
};
use twilight_model::{
    application::{
        command::{Command, CommandType},
        interaction::{InteractionContextType, application_command::CommandOptionValue},
    },
    channel::message::MessageFlags,
    gateway::payload::incoming::InteractionCreate,
};
use twilight_util::builder::command::{CommandBuilder, StringBuilder};

pub const NAME: &str = "discloud";

pub fn command() -> Command {
    let fetch = StringBuilder::new("fetch", "Select info to fetch")
        .choices([("status", "status"), ("logs", "logs")])
        .required(true)
        .build();

    CommandBuilder::new(
        NAME,
        "Show the bot status or logs from host",
        CommandType::ChatInput,
    )
    .option(fetch)
    .contexts([InteractionContextType::Guild])
    .build()
}

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    // Os logs do host podem conter informação sensível; antes qualquer membro podia ler.
    if !invoker_is_staff(interaction) {
        reply_with_embed(
            ctx,
            interaction,
            MessageFlags::EPHEMERAL,
            COLORS.danger,
            "You are not a mod or the owner of the guild.",
        )
        .await;
        return Ok(());
    }

    let Some(data) = get_app_command_data(interaction) else {
        return Ok(());
    };

    let fetch = data.options.first().and_then(|option| match &option.value {
        CommandOptionValue::String(value) => Some(value.as_str()),
        _ => None,
    });

    defer_reply(ctx, interaction, true).await;

    match fetch {
        Some("status") => update_status_panel(ctx, interaction).await,
        Some("logs") => update_logs_panel(ctx, interaction).await,
        _ => {}
    }

    Ok(())
}
