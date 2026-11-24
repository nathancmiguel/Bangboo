use serde::{Deserialize, Deserializer};

/// Os JSON de `data/` guardam IDs do Discord como string (para não perder precisão em JS).
/// Antes esta função estava copiada em emojis.rs, fab.rs e guild.rs.
pub fn str_to_u64<'de, D>(deserializer: D) -> Result<u64, D::Error>
where
    D: Deserializer<'de>,
{
    let s: &str = Deserialize::deserialize(deserializer)?;
    s.parse::<u64>().map_err(serde::de::Error::custom)
}
