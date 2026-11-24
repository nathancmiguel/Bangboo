using Bot.Services;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace Bot.Discord.Events;

public class MemberRemove(IOptions<Channels> channels, IGlobalService globalService, RestClient client) : IGuildUserRemoveGatewayHandler
{
    public async ValueTask HandleAsync(GuildUserRemoveEventArgs arg)
    {
        if (arg.User.IsBot) return;
        
        var guildBan = await client.GetGuildBanAsync(arg.GuildId, arg.User.Id);
        if (guildBan is null) return;

        var channel = (TextChannel)await client.GetChannelAsync(channels.Value.Announcement);
        var card = await globalService.GlobalMessage(arg.User, EMemberEvent.Leave);
        var stream = card.AsStream();
        var propriedadesMensagem = new MessageProperties()
        {
            Content = $"<t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:F>",
            Attachments = new[] { new AttachmentProperties("card.png", stream) }
        };
        await channel.SendMessageAsync(propriedadesMensagem);
    }
}