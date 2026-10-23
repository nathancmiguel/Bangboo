using Bot.Services;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace Bot.Discord.Events;

public class MemberUpdate(IOptions<Channels> channels, IOptions<Roles> roles, IGlobalService globalService, RestClient client) : IGuildUserUpdateGatewayHandler
{
    public async ValueTask HandleAsync(GuildUser arg)
    {
        if (arg.IsBot) return;

        if (arg.PremiumType == null)
        {
            await client.RemoveGuildUserRoleAsync(arg.GuildId, arg.Id, roles.Value.Boosters);
            return;
        }

        await client.AddGuildUserRoleAsync(arg.GuildId, arg.Id, roles.Value.Boosters);
        var channel = (TextChannel)await client.GetChannelAsync(channels.Value.Announcement);
        var card = await globalService.GlobalMessage(arg, EMemberEvent.Boost);
        var stream = card.AsStream();
        var propriedadesMensagem = new MessageProperties()
        {
            Content = $"<t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:F>",
            Attachments = new[] { new AttachmentProperties("card.png", stream) }
        };
        await channel.SendMessageAsync(propriedadesMensagem);
    }
}