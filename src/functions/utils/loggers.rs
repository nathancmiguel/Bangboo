use tracing_subscriber::EnvFilter;

/// Logs via `tracing`: é o que o twilight já usa internamente, então os eventos do gateway
/// e do HTTP passam a aparecer no mesmo lugar. Ajuste com `RUST_LOG` (ex.: `RUST_LOG=bot=debug`).
pub fn init_tracing() {
    let filter =
        EnvFilter::try_from_default_env().unwrap_or_else(|_| EnvFilter::new("warn,bot=info"));

    tracing_subscriber::fmt()
        .with_env_filter(filter)
        .with_target(false)
        .init();
}
