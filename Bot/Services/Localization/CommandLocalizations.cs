using NetCord.Services.ApplicationCommands;

namespace Bot.Services.Localization;

/// <summary>
/// Traduções dos slash commands enviadas ao Discord (descrições, parâmetros e escolhas).
/// Um arquivo por idioma em <c>Localizations/&lt;locale&gt;.json</c> (ex.: <c>pt-BR.json</c>), no
/// formato do <see cref="JsonLocalizationsProvider"/>: <c>commands</c> → nome do comando →
/// <c>description</c>/<c>parameters</c>, e <c>enums</c> → nome do enum → campo → texto.
/// Nomes de comandos e parâmetros não são traduzidos: <c>/profile</c> é igual em qualquer idioma.
/// </summary>
public static class CommandLocalizations
{
    /// <summary>Pasta copiada para a saída do build (ver <c>Bangboo.Bot.csproj</c>).</summary>
    public static string DefaultDirectory => Path.Combine(AppContext.BaseDirectory, "Localizations");

    public static ILocalizationsProvider CreateProvider(string? directory = null) =>
        new JsonLocalizationsProvider(new JsonLocalizationsProviderConfiguration
        {
            DirectoryPath = directory ?? DefaultDirectory,
        });
}
