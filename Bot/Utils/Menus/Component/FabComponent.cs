using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;

namespace Bot.Utils.Menus.Component;

public static class FabComponent
{
    public static IEnumerable<IMessageComponentProperties> Fab(
        IOptions<Colors> colors,
        IOptions<Emojis> emojis,
        IOptions<Fab> fab)
    {
        var engineUserSettings = fab.Value.EngineUserSetings;
        var internetProtocol = fab.Value.InternetProtocol;
 
        var engineUserSettingsRow = new ActionRowProperties()
            .AddComponents(
                new LinkButtonProperties(
                    Url(engineUserSettings?.ProductUrl, "Fab:EngineUserSettings:ProductUrl"),
                    "Engine User Settings",
                    Emoji(emojis.Value.Static.IconsF)),
                new LinkButtonProperties(
                    Url(engineUserSettings?.DocUrl, "Fab:EngineUserSettings:DocUrl"),
                    "Documentation",
                    Emoji(emojis.Value.Static.IconsD)));
 
        var internetProtocolRow = new ActionRowProperties()
            .AddComponents(
                new LinkButtonProperties(
                    Url(internetProtocol?.ProductUrl, "Fab:InternetProtocol:ProductUrl"),
                    "Internet Protocol",
                    Emoji(emojis.Value.Static.IconsF)),
                new LinkButtonProperties(
                    Url(internetProtocol?.DocUrl, "Fab:InternetProtocol:DocUrl"),
                    "Documentation",
                    Emoji(emojis.Value.Static.IconsD)));
 
        // Tudo dentro de um container (a barra colorida à esquerda vem do AccentColor)
        var container = new ComponentContainerProperties(
        [
            new TextDisplayProperties("### CHECK OUT MY FAB PRODUCTS"),
            new ComponentSeparatorProperties { Spacing = ComponentSeparatorSpacingSize.Small },
            engineUserSettingsRow,
            new TextDisplayProperties(Text(engineUserSettings?.ProductDesc, "Fab:EngineUserSettings:ProductDesc")),
            new ComponentSeparatorProperties { Spacing = ComponentSeparatorSpacingSize.Small },
            internetProtocolRow,
            new TextDisplayProperties(Text(internetProtocol?.ProductDesc, "Fab:InternetProtocol:ProductDesc"))
        ])
        {
            AccentColor = new Color(colors.Value.Green)
        };
 
        return [container];
    }
 
    private static string Text(string? value, string configKey) =>
        string.IsNullOrWhiteSpace(value)
            ? throw new InvalidOperationException($"Configuration value '{configKey}' is missing or empty.")
            : value;
 
    private static string Url(string? value, string configKey) =>
        Uri.TryCreate(value, UriKind.Absolute, out var uri) && (uri.Scheme is "http" or "https")
            ? uri.ToString()
            : throw new InvalidOperationException($"Configuration value '{configKey}' must be an absolute http(s) URL, but was '{value}'.");
 
    private static EmojiProperties Emoji(string value) =>
        EmojiProperties.Custom(ulong.TryParse(value, out var id)
            ? id
            : throw new InvalidOperationException($"Invalid emoji ID '{value}'."));
}