use super::de::str_to_u64;
use serde::{Deserialize, Serialize};
use twilight_model::id::{Id, marker::RoleMarker};

#[derive(Serialize, Deserialize)]
pub struct Guild {
    pub channels: Channels,

    pub roles: Roles,
}


#[derive(Serialize, Deserialize)]
pub struct Channels {
    #[serde(deserialize_with = "str_to_u64")]
    pub announcement: u64,
    #[serde(deserialize_with = "str_to_u64")]
    pub mousetrap: u64,
}

#[derive(Serialize, Deserialize)]
pub struct Roles {
    #[serde(deserialize_with = "str_to_u64")]
    pub apps: u64,
    #[serde(deserialize_with = "str_to_u64")]
    pub kernel: u64,
    #[serde(deserialize_with = "str_to_u64")]
    pub stf: u64,
    #[serde(deserialize_with = "str_to_u64")]
    pub boosters: u64,
}

impl Guild {
    /// Dono (kernel) ou moderador (stf). Centraliza a checagem que estava copiada
    /// em `moderate.rs` e `filters.rs`, cada uma com seus próprios `Lazy<Id<RoleMarker>>`.
    pub fn is_staff(&self, roles: &[Id<RoleMarker>]) -> bool {
        roles
            .iter()
            .any(|role| role.get() == self.roles.kernel || role.get() == self.roles.stf)
    }
}
