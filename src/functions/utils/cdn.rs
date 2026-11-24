use twilight_model::user::User;

/// URL do avatar customizado do usuário (`None` se ele usa o avatar padrão).
/// `size == 0` omite o parâmetro `?size=`.
pub fn display_avatar_url(user: &User, size: u16) -> Option<String> {
    let hash = user.avatar?;
    let url = format!("https://cdn.discordapp.com/avatars/{}/{}.png", user.id, hash);

    Some(if size == 0 {
        url
    } else {
        format!("{url}?size={size}")
    })
}
