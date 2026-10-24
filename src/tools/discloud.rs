use crate::{
    constants::*,
    discord::Context,
    functions::*,
    menus::components::{logs_component, status_component},
};
use regex::Regex;
use std::sync::LazyLock;
use twilight_model::{
    channel::message::MessageFlags, gateway::payload::incoming::InteractionCreate, util::Timestamp,
};

static ANSI_REGEX: LazyLock<Regex> =
    LazyLock::new(|| Regex::new(r"\x1b\[[0-9;]*m").expect("regex de códigos ANSI inválida"));

/// Um bloco de texto (TextDisplay) aceita no máximo 4000 caracteres; deixamos folga para
/// o título e a cerca de código, e mantemos o **final** do log (o mais recente).
const MAX_LOG_CHARS: usize = 3500;

fn tail_chars(text: &str, max: usize) -> &str {
    match text.char_indices().rev().nth(max.saturating_sub(1)) {
        Some((index, _)) => &text[index..],
        None => text,
    }
}

/// Linhas do painel de status. O `Err` já é uma mensagem pronta para o usuário.
async fn status_infos(ctx: &Context) -> Result<Vec<String>, &'static str> {
    let app = ctx.discloud.get_app(&APPID).await.map_err(|err| {
        tracing::error!(?err, "falha ao obter o app na Discloud");
        "Failed to get app"
    })?;

    let status = app.get_status(&ctx.discloud).await.map_err(|err| {
        tracing::error!(?err, "falha ao buscar o status do app na Discloud");
        "Failed to fetch app status"
    })?;

    let icons = &EMOJIS.emojis_static;

    let restart = match Timestamp::parse(&status.started_at) {
        Ok(timestamp) => format!("<t:{}:R>", timestamp.as_secs()),
        Err(_) => status.last_restart.to_string(),
    };

    Ok(vec![
        format!("<:id:{}>`Nome(ID):` **{}({})**", icons.id, app.name, app.id),
        format!("<:cpu:{}>`CPU:` **{}**", icons.cpu, status.cpu),
        format!("<:ram:{}>`RAM:` **{}**", icons.ram, status.memory),
        format!(
            "<:wifi:{}>`Network:`  `⬆`**{}** `⬇`**{}**",
            icons.wifi, status.net_io.up, status.net_io.down
        ),
        format!("<:refresh:{}>`Latest restart:` **{}**", icons.refresh, restart),
    ])
}

async fn logs_text(ctx: &Context) -> Result<String, &'static str> {
    let app = ctx.discloud.get_app(&APPID).await.map_err(|err| {
        tracing::error!(?err, "falha ao obter o app na Discloud");
        "Failed to get app"
    })?;

    let logs = app.get_logs(&ctx.discloud).await.map_err(|err| {
        tracing::error!(?err, "falha ao buscar os logs do app na Discloud");
        "Failed to fetch app logs"
    })?;

    let raw = logs.terminal.small.unwrap_or_default();
    let clean = ANSI_REGEX.replace_all(&raw, "");
    Ok(tail_chars(&clean, MAX_LOG_CHARS).to_owned())
}

/// Edita a resposta (já adiada) com o painel de status. Usado pelo comando e pelo botão "Refresh".
pub async fn update_status_panel(ctx: &Context, interaction: &InteractionCreate) {
    match status_infos(ctx).await {
        Ok(infos) => {
            update_v2(ctx, interaction, status_component(infos)).await;
        }
        Err(message) => {
            update_with_embed(ctx, interaction, MessageFlags::EPHEMERAL, COLORS.danger, message).await;
        }
    }
}

/// Idem, para o painel de logs.
pub async fn update_logs_panel(ctx: &Context, interaction: &InteractionCreate) {
    match logs_text(ctx).await {
        Ok(logs) => {
            update_v2(ctx, interaction, logs_component(&logs)).await;
        }
        Err(message) => {
            update_with_embed(ctx, interaction, MessageFlags::EPHEMERAL, COLORS.danger, message).await;
        }
    }
}
