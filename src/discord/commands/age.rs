use crate::{
    constants::COLORS,
    discord::Context,
    functions::{get_app_command_data, reply_with_embed},
    models::BotResult,
};
use twilight_model::{
    application::{
        command::{Command, CommandType},
        interaction::{InteractionContextType, application_command::CommandOptionValue},
    },
    channel::message::MessageFlags,
    gateway::payload::incoming::InteractionCreate,
};
use twilight_util::{
    builder::command::{CommandBuilder, UserBuilder},
    snowflake::Snowflake,
};

pub const NAME: &str = "age";

pub fn command() -> Command {
    let user_opt = UserBuilder::new("user", "Select an user").build();

    CommandBuilder::new(
        NAME,
        "Displays your or another user's account creation date.",
        CommandType::ChatInput,
    )
    .option(user_opt)
    .contexts([InteractionContextType::Guild])
    .build()
}

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let Some(data) = get_app_command_data(interaction) else {
        return Ok(());
    };

    let selected = data.options.iter().find_map(|option| match &option.value {
        CommandOptionValue::User(id) if option.name == "user" => Some(*id),
        _ => None,
    });

    // O Discord já manda o usuário escolhido em `resolved`: não precisa de cache nem de HTTP.
    // Antes, se o usuário não estivesse no cache o comando respondia com a data de quem o
    // executou, sem avisar.
    let user = match selected {
        Some(id) => data.resolved.as_ref().and_then(|resolved| resolved.users.get(&id)),
        None => interaction.author(),
    };

    let Some(user) = user else {
        reply_with_embed(
            ctx,
            interaction,
            MessageFlags::EPHEMERAL,
            COLORS.danger,
            "Could not find that user.",
        )
        .await;
        return Ok(());
    };

    let name = user.global_name.as_deref().unwrap_or(&user.name);
    let timestamp = user.id.timestamp() / 1000;

    let message = if interaction.locale.as_deref() == Some("pt-BR") {
        format!("**{name}** criou a conta <t:{timestamp}:R> em um(a) <t:{timestamp}:F> ")
    } else {
        format!("**{name}** account was created <t:{timestamp}:R> on <t:{timestamp}:F> ")
    };

    reply_with_embed(ctx, interaction, MessageFlags::empty(), COLORS.green, &message).await;
    Ok(())
}
