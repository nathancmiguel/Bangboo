using Bot.Utils.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;

namespace Bot.Services;

public interface IAutomodService
{
    Task Mousetrap(Message message);
}

/// <summary>
/// Mousetrap: qualquer mensagem no canal-armadilha (<c>channels.mousetrap</c> do <c>guild.json</c>) vira ban,
/// apagando 7 dias de mensagens. Pega contas hackeadas que saem mandando spam em todos os canais.
/// Quem está no servidor há mais de uma semana é desbanido em seguida (conta provavelmente invadida) e avisado por DM.
/// </summary>
public class AutomodService(
    IOptionsMonitor<Channels> channels,
    IOptionsMonitor<Colors> colors,
    RestClient client,
    ILogger<AutomodService> logger) : IAutomodService
{
    private const int DeletedMessageWindowSeconds = 60 * 60 * 24 * 7;
    private static readonly TimeSpan LongTimeMemberAge = TimeSpan.FromDays(7);
    private static readonly RestRequestProperties Reason = new() { AuditLogReason = "Captured by mousetrap." };

    private const string HackedAccountDm =
        "Hello! Bangboo has identified that your account sent a series of suspicious messages on the server's channels. " +
        "As you are a long-time member of the server, your ban has been automatically removed since your account was likely hacked. " +
        "I recommend that you review your account security!";

    public async Task Mousetrap(Message message)
    {
        if (message.ChannelId != channels.CurrentValue.Mousetrap || message.GuildId is not { } guildId)
        {
            return;
        }

        var user = message.Author;
        if (message.Guild is { } guild && user.Id == guild.OwnerId)
        {
            return; // o dono não pode ser banido
        }

        var joinedAt = (user as GuildUser)?.JoinedAt;
        var longTimeMember = joinedAt is { } joined && DateTimeOffset.UtcNow - joined > LongTimeMemberAge;

        // A DM vai antes do ban: depois dele o bot pode não dividir mais um servidor com o usuário.
        if (longTimeMember)
        {
            await TrySendDmAsync(user);
        }

        // Melhor esforço: o ban abaixo também apaga as mensagens recentes.
        try
        {
            await client.DeleteMessageAsync(message.ChannelId, message.Id, Reason);
        }
        catch (RestException exception)
        {
            logger.LogDebug(exception, "Mousetrap could not delete message from {UserId}", user.Id);
        }

        try
        {
            await client.BanGuildUserAsync(guildId, user.Id, DeletedMessageWindowSeconds, Reason);
        }
        catch (RestException exception)
        {
            // Sem permissão de banir ou o alvo tem cargo acima do bot.
            logger.LogError(exception, "Mousetrap could not ban user {UserId} in guild {GuildId}", user.Id, guildId);
            return;
        }

        if (longTimeMember)
        {
            try
            {
                await client.UnbanGuildUserAsync(guildId, user.Id, Reason);
            }
            catch (RestException exception)
            {
                logger.LogError(exception, "Mousetrap could not unban user {UserId} in guild {GuildId}", user.Id, guildId);
            }
        }

        logger.LogInformation("Mousetrap banned user {UserId} in guild {GuildId} (unbanned: {Unbanned})",
            user.Id, guildId, longTimeMember);
    }

    private async Task TrySendDmAsync(User user)
    {
        try
        {
            var dm = await user.GetDMChannelAsync();
            await dm.SendMessageAsync(new MessageProperties
            {
                Embeds = [new EmbedProperties { Description = HackedAccountDm, Color = new Color(colors.CurrentValue.Warning) }],
            });
        }
        catch (RestException exception)
        {
            // DMs fechadas: segue com o ban mesmo assim.
            logger.LogInformation(exception, "Mousetrap could not DM user {UserId}", user.Id);
        }
    }
}
