use crate::{
    constants::ids,
    discord::Context,
    functions::show_modal,
};
use twilight_model::{
    channel::message::{Component, component::SelectMenuType},
    gateway::payload::incoming::InteractionCreate,
    guild::Ban,
};
use twilight_util::builder::message::{
    LabelBuilder, SelectMenuBuilder, SelectMenuOptionBuilder, TextDisplayBuilder,
};

/// Limite de opções de um select menu do Discord.
pub const MAX_UNBAN_OPTIONS: usize = 25;

fn truncate(text: &str, max_chars: usize) -> String {
    text.chars().take(max_chars).collect()
}

pub async fn unban_modal(ctx: &Context, interaction: &InteractionCreate, bans: &[Ban]) {
    // Antes: `max_values(bans.len() as u8)` e uma opção por ban, o que quebra com mais de 25 bans.
    let shown = &bans[..bans.len().min(MAX_UNBAN_OPTIONS)];

    let mut menu = SelectMenuBuilder::new("users", SelectMenuType::Text)
        .max_values(shown.len() as u8)
        .min_values(1)
        .placeholder("Select at least one user");

    for ban in shown {
        let mut option =
            SelectMenuOptionBuilder::new(truncate(&ban.user.name, 100), ban.user.id.to_string());

        // Descrição vazia é rejeitada pela API (mín. 1 caractere); só definimos se houver motivo.
        if let Some(reason) = ban.reason.as_deref().filter(|reason| !reason.is_empty()) {
            option = option.description(truncate(reason, 100));
        }

        menu = menu.option(option.build());
    }

    let mut components = Vec::new();
    if bans.len() > shown.len() {
        components.push(Component::TextDisplay(
            TextDisplayBuilder::new(format!(
                "Showing the first {} of {} banned users.",
                shown.len(),
                bans.len()
            ))
            .build(),
        ));
    }
    components.push(Component::Label(
        LabelBuilder::new("User(s)", Component::SelectMenu(menu.build())).build(),
    ));

    show_modal(ctx, interaction, ids::MODAL_UNBAN, "Unban user(s)", components).await;
}
