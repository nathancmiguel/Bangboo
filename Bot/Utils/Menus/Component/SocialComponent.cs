using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;

namespace Bot.Utils.Menus.Component;

public static class SocialComponent
{
    public static IEnumerable<IMessageComponentProperties> Social(
        IOptions<Colors> colors,
        IOptions<Emojis> emojis,
        IOptions<Socials> social)
    {
        var icons = emojis.Value.Static;
        var links = social.Value;

        var socialRow = new ActionRowProperties()
            .AddComponents(
                new LinkButtonProperties(Url(links.Portfolio, "Socials:Portfolio"), "Portifolio", Emoji(icons.IconsP)),
                new LinkButtonProperties(Url(links.Youtube, "Socials:Youtube"), "Youtube", Emoji(icons.IconsYoutube)),
                new LinkButtonProperties(Url(links.Instagram, "Socials:Instagram"), "Instagram", Emoji(icons.IconsInstagram)),
                new LinkButtonProperties(Url(links.Github, "Socials:Github"), "Github", Emoji(icons.IconsGithub)),
                new LinkButtonProperties(Url(links.X, "Socials:X"), "X/Twitter", Emoji(icons.IconsX)));

        var fabRow = new ActionRowProperties()
            .AddComponents(
                new LinkButtonProperties(Url(links.Fab, "Socials:Fab"), "Fab", Emoji(icons.IconsF)));

        var container = new ComponentContainerProperties(
        [
            new TextDisplayProperties("## FOLLOW ME ON SOCIAL MEDIA"),
            new ComponentSeparatorProperties { Spacing = ComponentSeparatorSpacingSize.Small },
            socialRow,
            new ComponentSeparatorProperties { Spacing = ComponentSeparatorSpacingSize.Large, Divider = true },
            new TextDisplayProperties("## VISIT MY FAB STORE"),
            fabRow
        ])
        {
            AccentColor = new Color(colors.Value.Success)
        };

        return [container];
    }

    // Erro claro (com o nome da chave) em vez de "Invalid Form Body" do Discord
    private static string Url(string? value, string configKey) =>
        Uri.TryCreate(value, UriKind.Absolute, out var uri) && (uri.Scheme is "http" or "https")
            ? uri.AbsoluteUri // AbsoluteUri mantém %20; ToString() desfaria o escape
            : throw new InvalidOperationException($"Configuration value '{configKey}' must be an absolute http(s) URL, but was '{value}'.");

    private static EmojiProperties Emoji(string value) =>
        EmojiProperties.Custom(ulong.TryParse(value, out var id)
            ? id
            : throw new InvalidOperationException($"Invalid emoji ID '{value}'."));
}