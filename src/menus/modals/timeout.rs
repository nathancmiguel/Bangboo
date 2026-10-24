use super::common::*;
use crate::{
    constants::ids,
    discord::Context,
    functions::show_modal,
};
use twilight_model::{
    channel::message::{Component, component::SelectMenuType},
    gateway::payload::incoming::InteractionCreate,
};
use twilight_util::builder::message::{LabelBuilder, SelectMenuBuilder, SelectMenuOptionBuilder};

/// (rótulo, segundos). Fonte única: o handler do modal valida o valor recebido contra esta lista,
/// em vez de aceitar qualquer número em string.
pub const DURATIONS: [(&str, u64); 5] = [
    ("60 seconds", 60),
    ("05 minutes", 300),
    ("10 minutes", 600),
    ("01 hour", 3600),
    ("01 week", 604_800),
];

pub async fn timeout_modal(ctx: &Context, interaction: &InteractionCreate) {
    let mut menu = SelectMenuBuilder::new("duration", SelectMenuType::Text)
        .max_values(1)
        .min_values(1)
        .placeholder("Select duration");

    for (index, (label, seconds)) in DURATIONS.iter().enumerate() {
        menu = menu.option(
            SelectMenuOptionBuilder::new(*label, seconds.to_string())
                .default(index == 0)
                .build(),
        );
    }

    let duration_select = Component::Label(LabelBuilder::new("Duration", Component::SelectMenu(menu.build())).build());

    let mut components = staff_warning();
    components.push(user_select());
    components.push(duration_select);
    components.push(reason_input());

    show_modal(ctx, interaction, ids::MODAL_TIMEOUT, "Timeout user(s)", components).await;
}
