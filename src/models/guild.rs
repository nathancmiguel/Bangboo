use serde::Deserialize;
use twilight_model::id::{
    Id,
    marker::{ChannelMarker, RoleMarker},
};

/// IDs do servidor lidos de `data/guild.json`. O `Id` do twilight já aceita string e rejeita 0
/// na desserialização (validada em `constants::validate`), então não há `Id::new` (que dá panic) no código.
#[derive(Deserialize)]
pub struct Guild {
    pub channels: Channels,
    pub roles: Roles,
}

#[derive(Deserialize)]
pub struct Channels {
    pub announcement: Id<ChannelMarker>,
    pub mousetrap: Id<ChannelMarker>,
}

#[derive(Deserialize)]
pub struct Roles {
    pub kernel: Id<RoleMarker>,
    pub stf: Id<RoleMarker>,
    pub boosters: Id<RoleMarker>,
}

impl Guild {
    /// Dono (kernel) ou moderador (stf).
    pub fn is_staff(&self, roles: &[Id<RoleMarker>]) -> bool {
        roles
            .iter()
            .any(|&role| role == self.roles.kernel || role == self.roles.stf)
    }
}
