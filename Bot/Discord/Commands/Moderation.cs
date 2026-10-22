using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using Bot.Utils.Constants;
using Bot.Utils.Menus.Modals;
using Microsoft.Extensions.Options;
using EmbedBuilder = Bangboo.Utils.EmbedBuilder;
using Bot.Handler;

public enum ModAction
{
    [SlashCommandChoice(Name = "delete message")]
    DeleteMessage,
    [SlashCommandChoice(Name = "timeout")]
    Timeout,
    [SlashCommandChoice(Name = "kick")]
    Kick,
    [SlashCommandChoice(Name = "ban")]
    Ban,
    [SlashCommandChoice(Name = "unban")]
    Unban
}

public class ModerationModule(IOptions<Colors> colors) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("moderate", "Equality before the law is the cornerstone of justice ⚖.", Contexts = [InteractionContextType.Guild])]
    public async Task Moderate(ModAction action)
    {
        if (Context.Guild.OwnerId != Context.User.Id)
        {
            throw new BotException("You are not a mod or the owner of the guild.");
        }

        switch (action)
        {
            case ModAction.DeleteMessage:
                await Context.Interaction.SendResponseAsync(
                    InteractionCallback.Modal(ModerationMenus.DeleteMessageMenu())
                );
                break;
            case ModAction.Timeout:
                await Context.Interaction.SendResponseAsync(
                    InteractionCallback.Modal(ModerationMenus.TimeoutMenu())
                );
                break;
            case ModAction.Kick:
                await Context.Interaction.SendResponseAsync(
                    InteractionCallback.Modal(ModerationMenus.KickMenu())
                );
                break;
            case ModAction.Ban:
                await Context.Interaction.SendResponseAsync(
                    InteractionCallback.Modal(ModerationMenus.BanMenu())
                );
                break;
            case ModAction.Unban:
            {
                var bannedMembers = await Context.Guild.GetBansAsync(new() { BatchSize = 25 }).Take(25).ToListAsync();
                if (bannedMembers.Count == 0)
                {
                    var embed = EmbedBuilder.Res("There is no banned members in this guild", colors.Value.Danger);
                    await Context.Interaction.SendResponseAsync(
                        InteractionCallback.Message(new() { Embeds = [embed], Flags = MessageFlags.Ephemeral })
                    );
                    // Antes faltava o return: o código tentava responder a mesma interação 2x e estourava
                    return;
                }

                await Context.Interaction.SendResponseAsync(
                    InteractionCallback.Modal(ModerationMenus.UnbanMenu(bannedMembers))
                );
                break;
            }
        }
    }
}