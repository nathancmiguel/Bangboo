//! Peças comuns dos modais de moderação (antes copiadas em cada um dos 5 arquivos).

use twilight_model::channel::message::{
    Component,
    component::{SelectMenuType, TextInput, TextInputStyle},
};
use twilight_util::builder::message::{LabelBuilder, SelectMenuBuilder, TextDisplayBuilder};

/// Aviso exibido no topo dos modais que filtram dono/moderadores dos alvos.
pub(super) fn staff_warning() -> Vec<Component> {
    vec![
        Component::TextDisplay(TextDisplayBuilder::new("# Warning").build()),
        Component::TextDisplay(
            TextDisplayBuilder::new(
                "Bangboo will automatically filter and remove the guild owner and moderators if any are selected.",
            )
            .build(),
        ),
    ]
}

/// Seletor de até 25 usuários, com `custom_id` = "users".
pub(super) fn user_select() -> Component {
    Component::Label(
        LabelBuilder::new(
            "User(s)",
            Component::SelectMenu(
                SelectMenuBuilder::new("users", SelectMenuType::User)
                    .max_values(25)
                    .min_values(1)
                    .placeholder("Select at least one user")
                    .build(),
            ),
        )
        .build(),
    )
}

/// Campo de texto curto, com `custom_id` = "reason".
pub(super) fn reason_input() -> Component {
    #[allow(deprecated)] // `label` é obrigatório no struct, mas o rótulo real fica no `Label`.
    let input = TextInput {
        id: None,
        custom_id: "reason".to_string(),
        label: None,
        max_length: Some(500),
        min_length: None,
        placeholder: Some("Visible only in auditlogs channel".into()),
        required: Some(false),
        style: TextInputStyle::Short,
        value: None,
    };

    Component::Label(LabelBuilder::new("Reason", Component::TextInput(input)).build())
}
