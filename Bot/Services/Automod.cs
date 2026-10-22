using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord.Gateway;
using NetCord.Rest;

namespace Services;

public interface IAutomodService
{
    Task Mousetrap(Message message);
}

public class AutomodService(IOptions<Channels> channels) : IAutomodService
{
    private const int DeletedMessageWindowSeconds = 60 * 60 * 24 * 7;
    private const string LongTimeMemberMessage =
        "Hello! Bangboo has identified that your account sent a series of suspicious messages on the server's channels. " +
        "As you are a long-time member of the server, your ban has been automatically removed since your account was likely hacked. " +
        "I recommend that you review your account security!";

    public async Task Mousetrap(Message message)
    {
        if (message.ChannelId != channels.Value.Mousetrap)
        {
            return;
        }

        var guild = message.Guild;
        if (guild is null)
        {
            return;
        }
        var user = message.Author;
        var member = await guild.GetUserAsync(user.Id);
        var requestProps = new RestRequestProperties
        {
            AuditLogReason = "User caught by the mousetrap automod."
        };

        await guild.BanUserAsync(user.Id, DeletedMessageWindowSeconds, requestProps);

        if (member.JoinedAt is { } joinedAt &&
            joinedAt <= DateTimeOffset.UtcNow.AddDays(-7))
        {
            await guild.UnbanUserAsync(user.Id, requestProps);

            var dm = await user.GetDMChannelAsync();
            await dm.SendMessageAsync(new MessageProperties
            {
                Content = LongTimeMemberMessage
            });
        }
    }
}