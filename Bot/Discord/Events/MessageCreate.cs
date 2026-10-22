using Bot.Services;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Rest;
using Services;

namespace Bot.Discord.Events;

public class MessageCreateHandler(IAutomodService automodService, IGlobalService globalService, RestClient client) : IMessageCreateGatewayHandler
{
    public async ValueTask HandleAsync(Message arg)
    {
        if (arg.Author.IsBot) return;
        if (arg.GuildId is null) return;
        
        await automodService.Mousetrap(arg);
    }
}