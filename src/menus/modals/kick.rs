use super::common::*;
use crate::{
    constants::ids,
    discord::Context,
    functions::show_modal,
};
use twilight_model::gateway::payload::incoming::InteractionCreate;

pub async fn kick_modal(ctx: &Context, interaction: &InteractionCreate) {
    let mut components = staff_warning();
    components.push(user_select());
    components.push(reason_input());

    show_modal(ctx, interaction, ids::MODAL_KICK, "Kick user(s)", components).await;
}
