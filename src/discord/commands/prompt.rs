use crate::{
    constants::COLORS,
    discord::Context,
    functions::*,
    models::BotResult,
    tools::gemini::get_text,
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

pub const NAME: &str = "prompt";

/// Limite em **caracteres** por embed (a descrição de um embed aceita até 4096).
const CHUNK_CHARS: usize = 3000;

pub fn command() -> Command {
    // `required(true)`: antes a opção era opcional, mas o código fazia `options[0]` (panic se vazia).
    let text = CommandOption::from(
        StringBuilder::new("text", "Enter the text")
            .required(true)
            .build(),
    );

    CommandBuilder::new(NAME, "Ask or interact with IA", CommandType::ChatInput)
        .option(text)
        .contexts([InteractionContextType::Guild])
        .build()
}

/// Divide em pedaços de até `max` caracteres. A versão anterior fatiava por **bytes**
/// (`&response[i..end]`), o que dá panic quando o corte cai no meio de um caractere UTF-8
/// (qualquer acento em português, por exemplo).
fn chunks(text: &str, max: usize) -> Vec<String> {
    let chars: Vec<char> = text.chars().collect();
    chars.chunks(max).map(|chunk| chunk.iter().collect()).collect()
}

pub async fn run(ctx: &Context, interaction: &InteractionCreate) -> BotResult {
    let question = get_app_command_data(interaction)
        .and_then(|data| data.options.first())
        .and_then(|option| match &option.value {
            CommandOptionValue::String(value) => Some(value.as_str()),
            _ => None,
        });
    let Some(question) = question else {
        return Ok(());
    };

    defer_reply(ctx, interaction, true).await;

    let response = match get_text(question).await {
        Ok(response) => response,
        Err(err) => {
            tracing::error!(%err, "falha ao consultar o Gemini");
            update_with_embed(
                ctx,
                interaction,
                MessageFlags::EPHEMERAL,
                COLORS.danger,
                "Failed to get a response from the AI.",
            )
            .await;
            return Ok(());
        }
    };

    let mut parts = chunks(&response, CHUNK_CHARS).into_iter();
    let first = parts.next().unwrap_or_default();

    update_with_embed(ctx, interaction, MessageFlags::EPHEMERAL, COLORS.green, &first).await;
    for part in parts {
        followup_with_embed(ctx, interaction, MessageFlags::EPHEMERAL, COLORS.green, &part).await;
    }

    Ok(())
}
