pub mod discloud;
pub mod fab;
pub mod social;

pub use discloud::*;
pub use fab::*;
pub use social::*;

use twilight_model::{
    channel::message::{Component, EmojiReactionType, component::ButtonStyle},
    id::{Id, marker::EmojiMarker},
};
use twilight_util::builder::message::ButtonBuilder;

/// Botão de link com emoji customizado. Esse bloco de ~10 linhas estava repetido 15 vezes.
pub(crate) fn link_button(label: &str, emoji_id: u64, url: impl Into<String>) -> Component {
    Component::Button(
        ButtonBuilder::new(ButtonStyle::Link)
            .label(label)
            .emoji(EmojiReactionType::Custom {
                id: Id::<EmojiMarker>::new(emoji_id),
                name: None,
                animated: false,
            })
            .url(url)
            .build(),
    )
}
