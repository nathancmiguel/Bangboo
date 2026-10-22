using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using Bangboo.Utils;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using EmbedBuilder = Bangboo.Utils.EmbedBuilder;
using Bot.Handler;

public class ProfileModule(IOptions<Colors> colors) : ApplicationCommandModule<ApplicationCommandContext>
{
    
    [SlashCommand("profile", "Displays your or another member's info from guild", Contexts = [InteractionContextType.Guild])]
    public async Task Profile(
        [SlashCommandParameter(Name = "user", Description = "Selected user")] User? user = null
        )
    {
        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage());
        
        user ??= Context.User;

        long unixTimestamp = user.CreatedAt.ToUnixTimeSeconds();

        string ageMessage;
        if (Context.Interaction.UserLocale== "pt-BR")
        {
            ageMessage = $"**{user.Username}** criou a conta <t:{unixTimestamp}:R> em um(a) <t:{unixTimestamp}:F>";
        }
        else
        {
            ageMessage = $"**{user.Username}**'s account was created <t:{unixTimestamp}:R> on <t:{unixTimestamp}:F>";
        }
        
        var embed = EmbedBuilder.Res(ageMessage, colors.Value.Green);
        
        await Context.Interaction.ModifyResponseAsync(message =>
            message.AddEmbeds([embed])
        );
    }
}