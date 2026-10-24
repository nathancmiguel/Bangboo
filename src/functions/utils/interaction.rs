use crate::discord::Context;
use std::collections::HashMap;
use twilight_model::{
    application::interaction::{
        InteractionData,
        application_command::CommandData,
        modal::{ModalInteractionComponent, ModalInteractionData},
    },
    channel::message::{Component, Embed, MessageFlags},
    gateway::payload::incoming::InteractionCreate,
    http::interaction::{InteractionResponse, InteractionResponseData, InteractionResponseType},
    id::{Id, marker::UserMarker},
};
use twilight_util::builder::{InteractionResponseDataBuilder, embed::EmbedBuilder};

// ---------------------------------------------------------------------------------------------
// Acesso aos dados da interação
// ---------------------------------------------------------------------------------------------

pub fn get_app_command_data(interaction: &InteractionCreate) -> Option<&CommandData> {
    match interaction.data.as_ref()? {
        InteractionData::ApplicationCommand(command) => Some(command.as_ref()),
        _ => None,
    }
}

pub fn get_modal_data(interaction: &InteractionCreate) -> Option<&ModalInteractionData> {
    match interaction.data.as_ref()? {
        InteractionData::ModalSubmit(modal) => Some(modal.as_ref()),
        _ => None,
    }
}

/// Campos de um modal indexados pelo `custom_id`, **sem clonar** os componentes
/// (a versão anterior copiava cada componente para dentro de um `HashMap<String, _>`).
pub struct ModalFields<'a>(HashMap<&'a str, &'a ModalInteractionComponent>);

impl<'a> ModalFields<'a> {
    pub fn new(components: &'a [ModalInteractionComponent]) -> Self {
        let mut fields = HashMap::new();
        for component in components {
            Self::collect(component, &mut fields);
        }
        Self(fields)
    }

    /// Campos podem vir dentro de um `Label` (modais com Components V2) ou soltos.
    fn collect(
        component: &'a ModalInteractionComponent,
        fields: &mut HashMap<&'a str, &'a ModalInteractionComponent>,
    ) {
        match component {
            ModalInteractionComponent::Label(label) => Self::collect(&label.component, fields),
            other => {
                if let Some(id) = Self::custom_id(other) {
                    fields.insert(id, other);
                }
            }
        }
    }

    fn custom_id(component: &ModalInteractionComponent) -> Option<&str> {
        match component {
            ModalInteractionComponent::UserSelect(c) => Some(c.custom_id.as_str()),
            ModalInteractionComponent::StringSelect(c) => Some(c.custom_id.as_str()),
            ModalInteractionComponent::ChannelSelect(c) => Some(c.custom_id.as_str()),
            ModalInteractionComponent::MentionableSelect(c) => Some(c.custom_id.as_str()),
            ModalInteractionComponent::RoleSelect(c) => Some(c.custom_id.as_str()),
            ModalInteractionComponent::TextInput(c) => Some(c.custom_id.as_str()),
            _ => None,
        }
    }

    /// Usuários escolhidos em um `UserSelect`.
    pub fn users(&self, id: &str) -> Option<&'a [Id<UserMarker>]> {
        match *self.0.get(id)? {
            ModalInteractionComponent::UserSelect(select) => Some(select.values.as_slice()),
            _ => None,
        }
    }

    /// Valores escolhidos em um `StringSelect`.
    pub fn selected(&self, id: &str) -> Option<&'a [String]> {
        match *self.0.get(id)? {
            ModalInteractionComponent::StringSelect(select) => Some(select.values.as_slice()),
            _ => None,
        }
    }

    /// Texto digitado em um `TextInput`.
    pub fn text(&self, id: &str) -> Option<&'a str> {
        match *self.0.get(id)? {
            ModalInteractionComponent::TextInput(input) => Some(input.value.as_str()),
            _ => None,
        }
    }
}

// ---------------------------------------------------------------------------------------------
// Respostas
//
// Todas as funções logam o erro e devolvem `bool` (true = sucesso), como antes.
// ---------------------------------------------------------------------------------------------

fn embed(color: u32, content: &str) -> Embed {
    EmbedBuilder::new().color(color).description(content).build()
}

async fn respond(
    ctx: &Context,
    interaction: &InteractionCreate,
    kind: InteractionResponseType,
    data: Option<InteractionResponseData>,
    what: &str,
) -> bool {
    let response = InteractionResponse { kind, data };

    let result = ctx
        .http
        .interaction(interaction.application_id)
        .create_response(interaction.id, &interaction.token, &response)
        .await;

    if let Err(err) = result {
        tracing::error!(?err, "falha ao {what}");
        return false;
    }
    true
}

pub async fn reply(
    ctx: &Context,
    interaction: &InteractionCreate,
    payload: InteractionResponseData,
) -> bool {
    respond(
        ctx,
        interaction,
        InteractionResponseType::ChannelMessageWithSource,
        Some(payload),
        "responder a interação",
    )
    .await
}

pub async fn defer_reply(ctx: &Context, interaction: &InteractionCreate, ephemeral: bool) -> bool {
    let data = ephemeral.then(|| {
        InteractionResponseDataBuilder::new()
            .flags(MessageFlags::EPHEMERAL)
            .build()
    });

    respond(
        ctx,
        interaction,
        InteractionResponseType::DeferredChannelMessageWithSource,
        data,
        "adiar (defer) a interação",
    )
    .await
}

pub async fn defer_update_reply(ctx: &Context, interaction: &InteractionCreate) -> bool {
    respond(
        ctx,
        interaction,
        InteractionResponseType::DeferredUpdateMessage,
        None,
        "adiar (defer) a atualização da mensagem",
    )
    .await
}

/// Abre um modal como resposta da interação.
pub async fn show_modal(
    ctx: &Context,
    interaction: &InteractionCreate,
    custom_id: &str,
    title: &str,
    components: Vec<Component>,
) -> bool {
    let data = InteractionResponseData {
        custom_id: Some(custom_id.to_owned()),
        title: Some(title.to_owned()),
        components: Some(components),
        flags: Some(MessageFlags::IS_COMPONENTS_V2),
        ..Default::default()
    };

    respond(
        ctx,
        interaction,
        InteractionResponseType::Modal,
        Some(data),
        "abrir o modal",
    )
    .await
}

pub async fn update_reply(
    ctx: &Context,
    interaction: &InteractionCreate,
    payload: InteractionResponseData,
) -> bool {
    let interaction_api = ctx.http.interaction(interaction.application_id);
    let mut response = interaction_api.update_response(&interaction.token);

    if let Some(data) = &payload.allowed_mentions {
        response = response.allowed_mentions(Some(data));
    }
    if let Some(data) = &payload.attachments {
        response = response.attachments(data);
    }
    if let Some(data) = &payload.components {
        response = response.components(Some(data));
    }
    if let Some(data) = &payload.content {
        response = response.content(Some(data));
    }
    if let Some(data) = &payload.embeds {
        response = response.embeds(Some(data));
    }
    if let Some(data) = &payload.flags {
        response = response.flags(*data);
    }

    if let Err(err) = response.await {
        tracing::error!(?err, "falha ao atualizar a resposta da interação");
        return false;
    }
    true
}

#[cfg_attr(not(feature = "gemini"), allow(dead_code))]
pub async fn followup_reply(
    ctx: &Context,
    interaction: &InteractionCreate,
    payload: InteractionResponseData,
) -> bool {
    let interaction_api = ctx.http.interaction(interaction.application_id);
    let mut response = interaction_api.create_followup(&interaction.token);

    if let Some(data) = &payload.allowed_mentions {
        response = response.allowed_mentions(Some(data));
    }
    if let Some(data) = &payload.attachments {
        response = response.attachments(data);
    }
    if let Some(data) = &payload.components {
        response = response.components(data);
    }
    if let Some(data) = &payload.content {
        response = response.content(data);
    }
    if let Some(data) = &payload.embeds {
        response = response.embeds(data);
    }
    if let Some(data) = &payload.flags {
        response = response.flags(*data);
    }

    if let Err(err) = response.await {
        tracing::error!(?err, "falha ao enviar o followup da interação");
        return false;
    }
    true
}

pub async fn reply_with_embed(
    ctx: &Context,
    interaction: &InteractionCreate,
    flags: MessageFlags,
    color: u32,
    content: &str,
) -> bool {
    let payload = InteractionResponseDataBuilder::new()
        .embeds(vec![embed(color, content)])
        .flags(flags)
        .build();

    reply(ctx, interaction, payload).await
}

pub async fn update_with_embed(
    ctx: &Context,
    interaction: &InteractionCreate,
    flags: MessageFlags,
    color: u32,
    content: &str,
) -> bool {
    let payload = InteractionResponseDataBuilder::new()
        .embeds(vec![embed(color, content)])
        .flags(flags)
        .build();

    update_reply(ctx, interaction, payload).await
}

#[cfg_attr(not(feature = "gemini"), allow(dead_code))]
pub async fn followup_with_embed(
    ctx: &Context,
    interaction: &InteractionCreate,
    flags: MessageFlags,
    color: u32,
    content: &str,
) -> bool {
    let payload = InteractionResponseDataBuilder::new()
        .embeds(vec![embed(color, content)])
        .flags(flags)
        .build();

    followup_reply(ctx, interaction, payload).await
}

/// Resposta efêmera feita só de um componente V2 (fab, social...).
pub async fn reply_v2(ctx: &Context, interaction: &InteractionCreate, component: Component) -> bool {
    reply(ctx, interaction, v2_payload(component)).await
}

/// Mesma coisa, mas editando a resposta já adiada (defer).
pub async fn update_v2(ctx: &Context, interaction: &InteractionCreate, component: Component) -> bool {
    update_reply(ctx, interaction, v2_payload(component)).await
}

fn v2_payload(component: Component) -> InteractionResponseData {
    InteractionResponseDataBuilder::new()
        .flags(MessageFlags::EPHEMERAL | MessageFlags::IS_COMPONENTS_V2)
        .components(vec![component])
        .build()
}
