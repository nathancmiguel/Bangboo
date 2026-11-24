use crate::{constants::COLORS, functions::*};
use twilight_model::{
    channel::message::Embed,
    id::{Id, marker::UserMarker},
    user::User,
};
use twilight_util::builder::embed::{
    EmbedAuthorBuilder, EmbedBuilder, EmbedFieldBuilder, EmbedFooterBuilder, ImageSource,
};

const OFFICER_THUMBNAIL: &str = "https://raw.githubusercontent.com/BangbooBot/Bot/refs/heads/main/packages/bot/assets/avatar/Officer.png";

pub fn officer_cui_action_embed(
    moderator: &User,
    action: &str,
    success_users: &[Id<UserMarker>],
    failed_users: &[Id<UserMarker>],
    reason: &str,
) -> Embed {
    let name = moderator.global_name.as_ref().unwrap_or(&moderator.name);
    let mut author = EmbedAuthorBuilder::new(name);
    if let Some(url) = display_avatar_url(moderator, 0) {
        author = author.url(url);
    }

    let mut embed = EmbedBuilder::new()
        .color(COLORS.royal)
        .author(author.build())
        .title("**Officer Cui's panel**")
        .description(format!("### {action} action!"))
        .footer(EmbedFooterBuilder::new(format!("Reason: {reason}")).build());

    if let Ok(source) = ImageSource::url(OFFICER_THUMBNAIL) {
        embed = embed.thumbnail(source);
    }

    for (title, users) in [("**Success**", success_users), ("**Fail**", failed_users)] {
        if users.is_empty() {
            continue;
        }
        let mentions = users
            .iter()
            .map(|id| format!("<@{id}>"))
            .collect::<Vec<_>>()
            .join("\n");
        embed = embed.field(EmbedFieldBuilder::new(title, mentions).build());
    }

    embed.build()
}
