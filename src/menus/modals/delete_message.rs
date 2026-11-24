use super::common::user_select;
use crate::{
    constants::ids,
    discord::Context,
    functions::show_modal,
};
use twilight_model::{
    channel::message::{
        Component,
        component::{TextInput, TextInputStyle},
    },
    gateway::payload::incoming::InteractionCreate,
};
use twilight_util::builder::message::LabelBuilder;

/// Máximo de mensagens que a API devolve por requisição (e que o bulk delete aceita).
pub const MAX_DELETE_LIMIT: u16 = 100;

pub async fn delete_message_modal(ctx: &Context, interaction: &InteractionCreate) {
    #[allow(deprecated)]
    let limit_input = TextInput {
        id: None,
        custom_id: "limit".to_string(),
        label: None,
        max_length: Some(3),
        min_length: Some(1),
        // O placeholder antigo dizia "1 a 1000", mas a API do Discord limita a 100.
        placeholder: Some(format!("Numbers from 1 to {MAX_DELETE_LIMIT}")),
        required: Some(true),
        style: TextInputStyle::Short,
        value: Some("1".into()),
    };

    let components = vec![
        user_select(),
        Component::Label(LabelBuilder::new("Limit", Component::TextInput(limit_input)).build()),
    ];

    show_modal(
        ctx,
        interaction,
        ids::MODAL_DELETE_MESSAGE,
        "Delete message(s)",
        components,
    )
    .await;
}
