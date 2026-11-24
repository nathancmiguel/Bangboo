use super::link_button;
use crate::constants::{COLORS, EMOJIS};
use twilight_model::channel::message::{Component, component::SeparatorSpacingSize};
use twilight_util::builder::message::{
    ActionRowBuilder, ContainerBuilder, SeparatorBuilder, TextDisplayBuilder,
};

pub fn social_component() -> Component {
    let icons = &EMOJIS.emojis_static;

    let socials = [
        ("Portifolio", icons.icons_p, "https://nathan-miguel.vercel.app/"),
        ("Youtube", icons.icons_youtube, "https://www.youtube.com/@NathanMiguel1"),
        ("Instagram", icons.icons_instagram, "https://www.instagram.com/nathan_cmiguel/"),
        ("Github", icons.icons_github, "https://github.com/nathancmiguel"),
        ("X/Twitter", icons.icons_x, "https://x.com/nathancmig"),
    ];

    let social_row = socials
        .into_iter()
        .fold(ActionRowBuilder::new(), |row, (label, emoji, url)| {
            row.component(link_button(label, emoji, url))
        })
        .build();

    let fab_row = ActionRowBuilder::new()
        .component(link_button(
            "Fab",
            icons.icons_f,
            "https://www.fab.com/sellers/Nathan%20Miguel",
        ))
        .build();

    let separator = SeparatorBuilder::new()
        .spacing(SeparatorSpacingSize::Small)
        .build();
    let fab_separator = SeparatorBuilder::new()
        .spacing(SeparatorSpacingSize::Large)
        .divider(true)
        .build();

    Component::Container(
        ContainerBuilder::new()
            .accent_color(Some(COLORS.green))
            .component(Component::TextDisplay(
                TextDisplayBuilder::new("## FOLLOW ME ON SOCIAL MEDIA").build(),
            ))
            .component(Component::Separator(separator))
            .component(Component::ActionRow(social_row))
            .component(Component::Separator(fab_separator))
            .component(Component::TextDisplay(
                TextDisplayBuilder::new("## VISIT MY FAB STORE").build(),
            ))
            .component(Component::ActionRow(fab_row))
            .build(),
    )
}
