using Bot.Handler;
using Bot.Services;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace Bot.Discord.Events;

public class MemberAdd(IOptions<Channels> channels, IGlobalService globalService, RestClient client) : IGuildUserAddGatewayHandler
{
    public async ValueTask HandleAsync(GuildUser arg)
    {
        if (arg.IsBot) return;

        var channel = await client.GetChannelAsync(channels.Value.Announcement) as TextChannel
            ?? throw new BotException(
                $"The configured announcement channel ({channels.Value.Announcement}) is not a text channel.");
        var card = await globalService.GlobalMessage((User)arg, EMemberEvent.Join);
        var stream = card.AsStream();
        var propriedadesMensagem = new MessageProperties()
        {
            Content = $"<t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:F>",
            Attachments = new[] { new AttachmentProperties("card.png", stream) }
        };
        await channel.SendMessageAsync(propriedadesMensagem);
    }
}