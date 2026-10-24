use crate::data::settings::EColors;
use serenity::all::CreateEmbed;

pub fn res(color: u32, content: &str) -> CreateEmbed {
    CreateEmbed::new().color(color).description(content)
}
