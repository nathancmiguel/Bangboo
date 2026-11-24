use crate::constants::{COLORS, ids};
use twilight_model::channel::message::{
    Component,
    component::{ButtonStyle, SeparatorSpacingSize},
};
use twilight_util::builder::message::{
    ActionRowBuilder, ButtonBuilder, ContainerBuilder, SeparatorBuilder, TextDisplayBuilder,
};

/// Painel verde com título, corpo e um botão "Refresh".
/// `status_component` e `logs_component` eram cópias quase idênticas uma da outra.
fn refresh_panel(title: &str, body: String, refresh_id: &str) -> Component {
    let separator = SeparatorBuilder::new()
        .spacing(SeparatorSpacingSize::Large)
        .build();
    let refresh_row = ActionRowBuilder::new()
        .component(Component::Button(
            ButtonBuilder::new(ButtonStyle::Success)
                .label("Refresh")
                .custom_id(refresh_id)
                .build(),
        ))
        .build();

    Component::Container(
        ContainerBuilder::new()
            .accent_color(Some(COLORS.green))
            .component(Component::TextDisplay(TextDisplayBuilder::new(title).build()))
            .component(Component::Separator(separator.clone()))
            .component(Component::TextDisplay(TextDisplayBuilder::new(body).build()))
            .component(Component::Separator(separator))
            .component(Component::ActionRow(refresh_row))
            .build(),
    )
}

pub fn status_component(infos: Vec<String>) -> Component {
    refresh_panel(
        "### BANGBOO'S STATUS",
        infos.join("\n"),
        ids::DISCLOUD_STATUS_REFRESH,
    )
}

pub fn logs_component(logs: &str) -> Component {
    refresh_panel(
        "### BANGBOO'S LOGS",
        format!("```bash\n{logs}\n```"),
        ids::DISCLOUD_LOGS_REFRESH,
    )
}
