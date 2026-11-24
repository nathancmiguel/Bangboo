using Bot.Services;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;


namespace Bot.Discord.Events;

public class MessageCreateHandler(IAutomodService automodService) : IMessageCreateGatewayHandler
{
    public async ValueTask HandleAsync(Message arg)
    {
        if (arg.Author.IsBot) return;
        if (arg.GuildId is null) return;
        
        await automodService.Mousetrap(arg);
    }
}