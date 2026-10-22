using Bot.Utils.Constants;
using Bot.Utils.Menus.Component;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Bot.Discord.Commands;

public class FabModule(IOptions<Colors> colors, IOptions<Emojis> emojis, IOptions<Fab> fab) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("fab", "Products on fab marketplace")]
    public async Task Fab()
    {
        var component = FabComponent.Fab(colors, emojis, fab);
        await Context.Interaction.SendResponseAsync(InteractionCallback.Message(new InteractionMessageProperties
        {
            Flags = MessageFlags.IsComponentsV2 | MessageFlags.Ephemeral,
            Components = component,
            Content = null, 
            Embeds = null
        }));
    }
}