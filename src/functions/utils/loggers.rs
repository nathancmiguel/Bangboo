use std::io::IsTerminal;
use tracing_subscriber::EnvFilter;

/// Logs via `tracing` (o mesmo que o twilight usa internamente, então avisos do gateway e do
/// HTTP aparecem no mesmo lugar). Ajuste o nível com `RUST_LOG` (ex.: `RUST_LOG=Bangboo=debug`).
pub fn init_tracing() {
    // O target dos logs é o nome do binário (`Bangboo`), não o do pacote (`bot`).
    let filter = EnvFilter::try_from_default_env().unwrap_or_else(|_| {
        EnvFilter::new(format!("warn,{}=info", env!("CARGO_CRATE_NAME")))
    });

    tracing_subscriber::fmt()
        .with_env_filter(filter)
        .with_target(false)
        .compact()
        // Cores só no terminal: no painel da Discloud os códigos ANSI viram lixo no texto.
        .with_ansi(std::io::stdout().is_terminal())
        .init();

    install_panic_hook();
}

/// Um panic dentro de um handler não derruba o bot (cada evento roda na sua própria task),
/// mas sem isto ele apareceria só como texto cru no stderr, fora do formato dos logs.
fn install_panic_hook() {
    std::panic::set_hook(Box::new(|info| {
        let message = info
            .payload()
            .downcast_ref::<&str>()
            .copied()
            .or_else(|| info.payload().downcast_ref::<String>().map(String::as_str))
            .unwrap_or("(sem mensagem)");
        let location = info
            .location()
            .map(|location| format!("{}:{}", location.file(), location.line()))
            .unwrap_or_default();

        tracing::error!(%location, "✘ panic: {message}");
    }));
}
