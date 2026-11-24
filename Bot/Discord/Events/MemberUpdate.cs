using Bot.Handler;
using Bot.Services;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace Bot.Discord.Events;

/// <summary>
/// Mantém o cargo de booster em dia. O evento dispara a cada mudança de apelido/cargo,
/// então só age quando o cargo realmente precisa mudar (e só anuncia boosts novos).
/// </summary>
public class MemberUpdate(IOptions<Channels> channels, IOptions<Roles> roles, IGlobalService globalService, RestClient client) : IGuildUserUpdateGatewayHandler
{
    public async ValueTask HandleAsync(GuildUser arg)
    {
        if (arg.IsBot) return;

        var boosterRole = roles.Value.Boosters;
        var hasRole = arg.RoleIds.Contains(boosterRole);
        var isBoosting = arg.GuildBoostStart is not null;

        if (!isBoosting && hasRole)
        {
            await client.RemoveGuildUserRoleAsync(arg.GuildId, arg.Id, boosterRole);
            return;
        }

        if (!isBoosting || hasRole) return;

        await client.AddGuildUserRoleAsync(arg.GuildId, arg.Id, boosterRole);

        var channel = await client.GetChannelAsync(channels.Value.Announcement) as TextChannel
                      ?? throw new BotException(
                          $"The configured announcement channel ({channels.Value.Announcement}) is not a text channel.");
        var card = await globalService.GlobalMessage(arg, EMemberEvent.Boost);
        await channel.SendMessageAsync(new MessageProperties
        {
            Content = $"<t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:F>",
            Attachments = [new AttachmentProperties("card.png", card.AsStream())]
        });
    }
}
