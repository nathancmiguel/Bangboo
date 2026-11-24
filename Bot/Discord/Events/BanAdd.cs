using Bot.Handler;
using Bot.Services;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;

namespace Bot.Discord.Events;

public class BanAdd(IOptions<Channels> channels, IGlobalService globalService, RestClient client) : IGuildBanAddGatewayHandler
{
    public async ValueTask HandleAsync(GuildBanEventArgs arg)
    {
        if (arg.User.IsBot) return;

        var channel = await client.GetChannelAsync(channels.Value.Announcement) as TextChannel
                      ?? throw new BotException(
                          $"The configured announcement channel ({channels.Value.Announcement}) is not a text channel.");
        var card = await globalService.GlobalMessage(arg.User, EMemberEvent.Mod);
        var stream = card.AsStream();
        var propriedadesMensagem = new MessageProperties()
        {
            Content = $"<t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:F>",
            Attachments = new[] { new AttachmentProperties("card.png", stream) }
        };
        await channel.SendMessageAsync(propriedadesMensagem);
    }
}