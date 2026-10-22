using Bot.Utils.Constants;
using Bot.Utils.Menus.Component;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Bot.Discord.Commands;

public class SocialModule(IOptions<Colors> colors, IOptions<Emojis> emojis, IOptions<Socials> social) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("social", "Social media")]
    public async Task Fab()
    {
        var component = SocialComponent.Social(colors, emojis, social);
        await Context.Interaction.SendResponseAsync(InteractionCallback.Message(new InteractionMessageProperties
        {
            Flags = MessageFlags.IsComponentsV2 | MessageFlags.Ephemeral,
            Components = component,
            Content = null, 
            Embeds = null
        }));
    }
}