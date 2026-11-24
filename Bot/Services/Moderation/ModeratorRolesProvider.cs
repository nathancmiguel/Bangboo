using Bot.Handler;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;

namespace Bot.Services.Moderation;

/// <summary>Cargos que podem usar o <c>/moderate</c> (kernel e stf, de <c>Data/guild.json</c>).</summary>
public interface IModeratorRolesProvider
{
    IReadOnlySet<ulong> Get();
}

public sealed class ModeratorRolesProvider(IOptionsMonitor<Roles> roles) : IModeratorRolesProvider
{
    public IReadOnlySet<ulong> Get() => new HashSet<ulong> { roles.CurrentValue.Kernel, roles.CurrentValue.Stf };
}

public static class ModeratorRolesProviderExtensions
{
    /// <summary>
    /// Lança <see cref="BotException"/> se o usuário não puder usar o <c>/moderate</c> nesta guild.
    /// Devolve a guild já validada (não nula).
    /// </summary>
    public static Task<NetCord.Gateway.Guild> EnsureCanModerateAsync(
        this IModeratorRolesProvider provider,
        NetCord.Gateway.Guild? guild,
        User user)
    {
        if (guild is null || user is not GuildInteractionUser member)
        {
            throw new BotException("This command can only be used in a server.");
        }

        var canModerate = ModerationPolicy.CanModerate(
            isOwner: guild.OwnerId == member.Id,
            isAdministrator: member.Permissions.HasFlag(Permissions.Administrator),
            member.RoleIds,
            provider.Get());

        if (!canModerate)
        {
            throw new BotException("You don't have a moderator role in this server.");
        }

        return Task.FromResult(guild);
    }
}
