pub mod ban_add;
pub mod member_add;
pub mod member_remove;
pub mod member_update;
pub mod message_create;
pub mod ready;

/// Eventos com handler (usado no log de inicialização; o despacho fica em `base/router.rs`).
pub const NAMES: &[&str] = &[
    "ready",
    "ban_add",
    "member_add",
    "member_remove",
    "member_update",
    "message_create",
];
