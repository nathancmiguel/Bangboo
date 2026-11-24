using System.Text.RegularExpressions;
using Bot.Handler;
using Bot.Utils.Constants;
using Bot.Utils.Menus.Component;
using Discloud;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCord.Rest;

namespace Bot.Services;

public interface IDiscloudPanelService
{
    Task<IEnumerable<IMessageComponentProperties>> StatusAsync(CancellationToken ct = default);
    Task<IEnumerable<IMessageComponentProperties>> LogsAsync(CancellationToken ct = default);
}

/// <summary>Monta os painéis de status e logs do app na Discloud (comando <c>/discloud</c> e botões "Refresh").</summary>
public partial class DiscloudPanelService(
    IDiscloudClient discloud,
    IOptionsMonitor<DiscloudApp> app,
    IOptionsMonitor<Colors> colors,
    IOptionsMonitor<Emojis> emojis,
    ILogger<DiscloudPanelService> logger) : IDiscloudPanelService
{
    // Um TextDisplay aceita até 4000 caracteres: sobra espaço para o título e a cerca de código,
    // e mantemos o final do log (o mais recente).
    private const int MaxLogChars = 3500;

    [GeneratedRegex(@"\x1b\[[0-9;]*m")]
    private static partial Regex AnsiRegex();

    public async Task<IEnumerable<IMessageComponentProperties>> StatusAsync(CancellationToken ct = default)
    {
        var appId = AppId();
        var (info, status) = await CallAsync(async () =>
        {
            var info = await discloud.Apps.GetAsync(appId, ct);
            var status = await discloud.Apps.GetStatusAsync(appId, ct);
            return (info, status);
        }, "Failed to fetch app status");

        var icons = emojis.CurrentValue.Static;
        var restart = status.StartedAt is { } startedAt
            ? $"<t:{startedAt.ToUnixTimeSeconds()}:R>"
            : status.LastRestart ?? "-";

        string[] lines =
        [
            $"<:id:{icons.Id}>`Nome(ID):` **{info.Name}({info.Id})**",
            $"<:cpu:{icons.Cpu}>`CPU:` **{status.Cpu}**",
            $"<:ram:{icons.Ram}>`RAM:` **{status.Memory}**",
            $"<:wifi:{icons.Wifi}>`Network:`  `⬆`**{status.NetIO?.Up}** `⬇`**{status.NetIO?.Down}**",
            $"<:refresh:{icons.Refresh}>`Latest restart:` **{restart}**",
        ];

        return DiscloudComponent.Status(lines, colors.CurrentValue.Green);
    }

    public async Task<IEnumerable<IMessageComponentProperties>> LogsAsync(CancellationToken ct = default)
    {
        var appId = AppId();
        var logs = await CallAsync(() => discloud.Apps.GetLogsAsync(appId, ct), "Failed to fetch app logs");

        var clean = AnsiRegex().Replace(logs.Terminal?.Small ?? string.Empty, string.Empty);
        if (clean.Length > MaxLogChars)
        {
            clean = clean[^MaxLogChars..];
        }

        return DiscloudComponent.Logs(clean, colors.CurrentValue.Green);
    }

    private string AppId() =>
        string.IsNullOrWhiteSpace(app.CurrentValue.AppId)
            ? throw new BotException("Discloud:AppId is not configured.", deferred: true)
            : app.CurrentValue.AppId;

    // Erros da Discloud viram uma mensagem curta para o usuário; o detalhe fica no log.
    private async Task<T> CallAsync<T>(Func<Task<T>> call, string userMessage)
    {
        try
        {
            return await call();
        }
        catch (DiscloudException exception)
        {
            logger.LogError(exception, "{Message}", userMessage);
            throw new BotException(userMessage, deferred: true, exception);
        }
    }
}
