using NetCord;
using NetCord.Rest;

namespace Bot.Utils.Menus.Component;

public static class DiscloudComponent
{
    public const string StatusRefreshId = "discloud/status/refresh";
    public const string LogsRefreshId = "discloud/logs/refresh";

    public static IEnumerable<IMessageComponentProperties> Status(IEnumerable<string> lines, int color) =>
        RefreshPanel("### BANGBOO'S STATUS", string.Join('\n', lines), StatusRefreshId, color);

    public static IEnumerable<IMessageComponentProperties> Logs(string logs, int color) =>
        RefreshPanel("### BANGBOO'S LOGS", $"```bash\n{logs}\n```", LogsRefreshId, color);

    /// <summary>Painel com título, corpo e um botão "Refresh".</summary>
    private static IEnumerable<IMessageComponentProperties> RefreshPanel(string title, string body, string refreshId, int color)
    {
        var refreshRow = new ActionRowProperties()
            .AddComponents(new ButtonProperties(refreshId, "Refresh", ButtonStyle.Success));

        var container = new ComponentContainerProperties(
        [
            new TextDisplayProperties(title),
            new ComponentSeparatorProperties { Spacing = ComponentSeparatorSpacingSize.Large },
            new TextDisplayProperties(body),
            new ComponentSeparatorProperties { Spacing = ComponentSeparatorSpacingSize.Large },
            refreshRow
        ])
        {
            AccentColor = new Color(color)
        };

        return [container];
    }
}
