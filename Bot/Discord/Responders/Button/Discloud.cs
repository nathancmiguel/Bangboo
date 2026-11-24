using Bot.Handler;
using Bot.Services;
using Bot.Services.Moderation;
using Bot.Utils.Constants;
using Bot.Utils.Menus.Component;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;

namespace Bot.Discord.Responders.Button;

/// <summary>Botões "Refresh" dos painéis do <c>/discloud</c>: atualizam a própria mensagem.</summary>
public class DiscloudRefreshModule(IModeratorRolesProvider moderatorRoles, IDiscloudPanelService panel, IOptions<Colors> colors)
    : ComponentInteractionModule<ButtonInteractionContext>
{
    [ComponentInteraction(DiscloudComponent.StatusRefreshId)]
    public Task RefreshStatus() => RefreshAsync(panel.StatusAsync);

    [ComponentInteraction(DiscloudComponent.LogsRefreshId)]
    public Task RefreshLogs() => RefreshAsync(panel.LogsAsync);

    private async Task RefreshAsync(Func<CancellationToken, Task<IEnumerable<IMessageComponentProperties>>> build)
    {
        await moderatorRoles.EnsureCanModerateAsync(Context.Guild, Context.User);

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredModifyMessage);

        IEnumerable<IMessageComponentProperties> components;
        try
        {
            components = await build(CancellationToken.None);
        }
        catch (BotException exception)
        {
            // Mensagem em components V2 não aceita "content": o erro vai num container.
            components =
            [
                new ComponentContainerProperties([new TextDisplayProperties(exception.Message)])
                {
                    AccentColor = new Color(colors.Value.Danger)
                }
            ];
        }

        await Context.Interaction.ModifyResponseAsync(message => message.Components = components);
    }
}
