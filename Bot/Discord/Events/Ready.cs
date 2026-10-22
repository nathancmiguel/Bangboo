using Microsoft.Extensions.Logging;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace Bot.Discord.Events;

public class ReadyGatewayHandler(ILogger<ReadyEventArgs> logger) : IReadyGatewayHandler
{
    public async ValueTask HandleAsync(ReadyEventArgs arg)
    {
        
        logger.LogInformation($"● {arg.User.Username} online ✓");
    }
}
