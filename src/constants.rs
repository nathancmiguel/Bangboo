use crate::models::*;
use std::sync::LazyLock;

// Member
pub const IMG_DEFAULT_AVATAR: &[u8] = include_bytes!("../assets/member/default_avatar.png");

// Fonts
pub const FONT_FREDOKA: &[u8] = include_bytes!("../assets/fonts/Fredoka-Medium.ttf");
pub const FONT_ROBOTO: &[u8] = include_bytes!("../assets/fonts/Roboto-Medium.ttf");

// Cards
pub const CARD_LEFT: &[u8] = include_bytes!("../assets/cards/card-left.png");
pub const CARD_MOD: &[u8] = include_bytes!("../assets/cards/card-mod.png");
pub const CARD_NEW: &[u8] = include_bytes!("../assets/cards/card-new.png");
pub const CARD_BOOST: &[u8] = include_bytes!("../assets/cards/card-boost.png");

/// `custom_id`s compartilhados entre quem monta o componente (menus/) e quem o trata (discord/).
/// Antes eram strings repetidas nos dois lados: um typo virava um botão/modal que "não faz nada".
pub mod ids {
    pub const MODAL_BAN: &str = "modal/moderate/ban";
    pub const MODAL_KICK: &str = "modal/moderate/kick";
    pub const MODAL_TIMEOUT: &str = "modal/moderate/timeout";
    pub const MODAL_UNBAN: &str = "modal/moderate/unban";
    pub const MODAL_DELETE_MESSAGE: &str = "modal/moderate/delete-message";

    pub const DISCLOUD_STATUS_REFRESH: &str = "discloud/status/refresh";
    pub const DISCLOUD_LOGS_REFRESH: &str = "discloud/logs/refresh";
}

// Discloud
const DISCLOUD_CONFIG: &str = include_str!("../discloud.config");

/// Lê `ID=...` do discloud.config. O arquivo é um `CHAVE=valor` simples:
/// não precisava da crate `config` (e do parser INI) só para isso.
pub static APPID: LazyLock<String> = LazyLock::new(|| {
    DISCLOUD_CONFIG
        .lines()
        .filter_map(|line| line.split_once('='))
        .find(|(key, _)| key.trim().eq_ignore_ascii_case("ID"))
        .map(|(_, value)| value.trim().to_owned())
        .unwrap_or_default()
});

// JSON embutidos
const COLORS_JSON: &str = include_str!("../data/colors.json");
pub static COLORS: LazyLock<Colors> = LazyLock::new(|| {
    serde_json::from_str::<Constants>(COLORS_JSON)
        .expect("data/colors.json inválido")
        .colors
});

const EMOJIS_JSON: &str = include_str!("../data/emojis.json");
pub static EMOJIS: LazyLock<Emojis> =
    LazyLock::new(|| serde_json::from_str(EMOJIS_JSON).expect("data/emojis.json inválido"));

const FAB_JSON: &str = include_str!("../data/fab.json");
pub static FAB: LazyLock<Fab> =
    LazyLock::new(|| serde_json::from_str(FAB_JSON).expect("data/fab.json inválido"));

const GUILD_JSON: &str = include_str!("../data/guild.json");
pub static GUILD: LazyLock<Guild> =
    LazyLock::new(|| serde_json::from_str(GUILD_JSON).expect("data/guild.json inválido"));

/// Força o parse de todos os JSON embutidos na inicialização.
pub fn validate() -> BotResult {
    fn check<T: serde::de::DeserializeOwned>(name: &str, raw: &str) -> BotResult {
        serde_json::from_str::<T>(raw).map(drop).map_err(|err| format!("{name}: {err}").into())
    }

    check::<Constants>("data/colors.json", COLORS_JSON)?;
    check::<Emojis>("data/emojis.json", EMOJIS_JSON)?;
    check::<Fab>("data/fab.json", FAB_JSON)?;
    check::<Guild>("data/guild.json", GUILD_JSON)?;
    Ok(())
}
