using Bot.Services;
using Bot.Services.Moderation;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Bot.Discord.Commands;

public enum DiscloudFetch
{
    [SlashCommandChoice(Name = "status")]
    Status,
    [SlashCommandChoice(Name = "logs")]
    Logs
}

public class DiscloudModule(IModeratorRolesProvider moderatorRoles, IDiscloudPanelService panel)
    : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("discloud", "Show the bot status or logs from host", Contexts = [InteractionContextType.Guild])]
    public async Task Discloud(
        [SlashCommandParameter(Name = "fetch", Description = "Select info to fetch")] DiscloudFetch fetch)
    {
        // Os logs do host podem ter informação sensível: só staff.
        await moderatorRoles.EnsureCanModerateAsync(Context.Guild, Context.User);

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Ephemeral));

        var components = fetch == DiscloudFetch.Status
            ? await panel.StatusAsync()
            : await panel.LogsAsync();

        await Context.Interaction.ModifyResponseAsync(message =>
        {
            message.Flags = MessageFlags.IsComponentsV2;
            message.Components = components;
        });
    }
}
