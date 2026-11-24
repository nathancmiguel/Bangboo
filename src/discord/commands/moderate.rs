use crate::{
    constants::COLORS,
    discord::Context,
    functions::{filters::invoker_is_staff, get_app_command_data, reply_with_embed},
    menus::*,
    models::BotResult,
};
use twilight_model::{
    application::{
        command::{Command, CommandOption, CommandType},
        interaction::{InteractionContextType, application_command::CommandOptionValue},
    },
    channel::message::MessageFlags,
    gateway::payload::incoming::InteractionCreate,
};
use twilight_util::builder::command::{CommandBuilder, StringBuilder};

pub const NAME: &str = "moderate";

pub fn command() -> Command {
    let action = CommandOption::from(
        StringBuilder::new("action", "Select an action.")
            .required(true)
            .choices([
                ("delete messages", "delete_messages"),
                ("timeout", "timeout"),
                ("kick", "kick"),
                ("ban", "ban"),
                ("unban", "unban"),
            ]),
    );

    CommandBuilder::new(
        NAME,
        "Equality before the law is the cornerstone of justice ⚖.",
        CommandType::ChatInput,
    )
    .option(action)
    .contexts([InteractionContextType::Guild])
    .build()
}

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    // Resposta efêmera: antes a recusa era pública e todo o canal via quem tentou usar o comando.
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

    let action = get_app_command_data(interaction)
        .and_then(|data| data.options.first())
        .and_then(|option| match &option.value {
            CommandOptionValue::String(value) => Some(value.as_str()),
            _ => None,
        });

    match action {
        Some("delete_messages") => delete_message_modal(ctx, interaction).await,
        Some("timeout") => timeout_modal(ctx, interaction).await,
        Some("kick") => kick_modal(ctx, interaction).await,
        Some("ban") => ban_modal(ctx, interaction).await,
        Some("unban") => {
            let Some(guild_id) = interaction.guild_id else {
                tracing::error!("interação de /moderate sem guild_id");
                return Ok(());
            };

            let bans = ctx.http.bans(guild_id).await?.model().await?;
            if bans.is_empty() {
                reply_with_embed(
                    ctx,
                    interaction,
                    MessageFlags::EPHEMERAL,
                    COLORS.danger,
                    "There is no banned users on this guild.",
                )
                .await;
                return Ok(());
            }

            unban_modal(ctx, interaction, &bans).await;
        }
        _ => {
            reply_with_embed(
                ctx,
                interaction,
                MessageFlags::EPHEMERAL,
                COLORS.danger,
                "Error trying to access unknown option.",
            )
            .await;
        }
    }

    Ok(())
}
