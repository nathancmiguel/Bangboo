using NetCord;
using NetCord.Rest;

namespace Bangboo.Utils;

public class EmbedBuilder
{
    public static EmbedProperties OfficerCuiAction(User user, string action, List<string> successList, List<string> failList, string reason = "")
    {
        var author = new EmbedAuthorProperties()
            .WithName(user.GlobalName ?? user.Username)
            .WithIconUrl(user.GetAvatarUrl().ToString());
        
        var embed = new EmbedProperties()
                .WithAuthor(author)
                .WithTitle("**Officer Cui's panel**")
                .WithColor(new Color(0x1447E6));

        if (successList.Count > 0 || failList.Count > 0)
        {
            var description = $"### {action} action!\n";
            if (successList.Count > 0)
                description = description + "\n" + "**Success**" + "\n" + string.Join('\n', successList);
            if (failList.Count > 0)
                description = description + "\n" + "**Failed**" + "\n" + string.Join('\n', failList);
            embed.WithDescription(description);
        }
        
        var footer = new EmbedFooterProperties()
            .WithText($"Reason: {reason}");
        
        embed.WithFooter(footer);
        
        return embed;
    }
    
    public static EmbedProperties Res(string message, int color)
    {
        return new EmbedProperties()
        {
            Description =  message,
            Color = new Color(color)
        };
    }
}