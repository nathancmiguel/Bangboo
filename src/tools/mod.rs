pub mod automod;
pub mod discloud;
#[cfg(feature = "gemini")]
pub mod gemini;

pub use automod::*;
