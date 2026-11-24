use crate::{constants::*, discord::Context, functions::skia::*, functions::*};
use skia_safe::{EncodedImageFormat, ISize, Point};
use twilight_gateway::EventType;
use twilight_model::{
    guild::Member,
    http::attachment::Attachment,
    id::{Id, marker::ChannelMarker},
    user::User,
};

/// Baixa o avatar do usuário (com timeout, via `reqwest::Client` compartilhado).
/// `None` = usar o avatar padrão (embutido no binário, sem cópia).
async fn fetch_avatar(ctx: &Context, user: &User) -> Option<impl AsRef<[u8]> + Send + 'static> {
    let url = display_avatar_url(user, 512)?;

    // `error_for_status` evita tratar uma página de erro (404/429) como se fosse uma imagem.
    let bytes = async {
        let response = ctx.web.get(url).send().await?.error_for_status()?;
        response.bytes().await
    }
    .await;

    match bytes {
        Ok(bytes) if !bytes.is_empty() => Some(bytes),
        Ok(_) => None,
        Err(err) => {
            tracing::warn!(%err, "falha ao baixar o avatar; usando o avatar padrão");
            None
        }
    }
}

/// Renderiza o card em PNG. É trabalho de CPU (decodificar, redimensionar, desenhar texto e
/// codificar um PNG 2800x560), então roda em `spawn_blocking` e não trava as threads async.
/// Como os objetos do Skia nascem e morrem dentro desta função, também some a
/// necessidade do "escopo para evitar problema com o skia" que existia antes.
fn render_card(
    background: &[u8],
    avatar: &[u8],
    name: &str,
    nickname: &str,
) -> Result<Vec<u8>, &'static str> {
    let mut surface = skia_safe::surfaces::raster_n32_premul(ISize {
        width: 2800,
        height: 560,
    })
    .ok_or("failed to create canvas surface")?;
    let canvas = surface.canvas();

    let background = load_image_from_bytes(background).ok_or("failed to load background image")?;
    canvas.draw_image(&background, Point { x: 0.0, y: 0.0 }, None);

    // Se o avatar baixado estiver corrompido, cai para o padrão em vez de abortar o card.
    let avatar = load_image_from_bytes(avatar)
        .or_else(|| load_image_from_bytes(IMG_DEFAULT_AVATAR))
        .ok_or("failed to decode user avatar image")?;
    let avatar = resize_image(&avatar, 400, 400).ok_or("failed to resize user avatar image")?;
    draw_circle(canvas, &avatar, 204.0, 360.0, 200.0);

    let fredoka = font_collection(FONT_FREDOKA).ok_or("failed to load title font")?;
    let roboto = font_collection(FONT_ROBOTO).ok_or("failed to load subtitle font")?;
    draw_text(canvas, &fredoka, name, 200.0, 530.0, 140.0);
    draw_text(canvas, &roboto, &format!("@{nickname}"), 96.0, 530.0, 380.0);

    surface
        .image_snapshot()
        .encode(None, EncodedImageFormat::PNG, Some(100))
        .map(|data| data.to_vec())
        .ok_or("failed to encode card image")
}

pub async fn global_message(
    ctx: &Context,
    channel_id: Id<ChannelMarker>,
    event: EventType,
    member: Option<&Member>,
    user: &User,
) {
    let background = match event {
        EventType::MemberAdd => CARD_NEW,
        EventType::BanAdd => CARD_MOD,
        EventType::MemberRemove => CARD_LEFT,
        _ => CARD_BOOST,
    };

    let avatar = fetch_avatar(ctx, user).await;
    let name = user.name.clone();
    let nickname = user
        .global_name
        .clone()
        .unwrap_or_else(|| "Undefined".to_owned());

    let card = tokio::task::spawn_blocking(move || {
        let avatar = avatar.as_ref().map_or(IMG_DEFAULT_AVATAR, |bytes| bytes.as_ref());
        render_card(background, avatar, &name, &nickname)
    })
    .await;

    let png = match card {
        Ok(Ok(png)) => png,
        Ok(Err(reason)) => {
            tracing::error!(reason, "falha ao gerar o card");
            return;
        }
        Err(err) => {
            tracing::error!(%err, "a tarefa de renderização do card falhou");
            return;
        }
    };

    // Entrada: horário em que o membro entrou; demais eventos: agora.
    let timestamp = match (event, member.and_then(|m| m.joined_at)) {
        (EventType::MemberAdd, Some(joined_at)) => joined_at.as_secs(),
        _ => now_secs(),
    };
    let content = format!("<t:{timestamp}:F>");

    let attachment = Attachment::from_bytes("Card.png".to_string(), png, 0);

    if let Err(err) = ctx
        .http
        .create_message(channel_id)
        .attachments(&[attachment])
        .content(&content)
        .await
    {
        tracing::error!(%err, "falha ao enviar o card");
    }
}
