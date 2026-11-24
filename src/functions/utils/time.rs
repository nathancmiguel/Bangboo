use std::time::{SystemTime, UNIX_EPOCH};

/// Segundos desde a epoch Unix. Substitui o uso do `chrono` só para pegar "agora".
pub fn now_secs() -> i64 {
    SystemTime::now()
        .duration_since(UNIX_EPOCH)
        .map(|elapsed| elapsed.as_secs() as i64)
        .unwrap_or_default()
}
