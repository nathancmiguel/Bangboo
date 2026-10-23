using Bangboo.Utils;
using Bot.Utils.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Rest;
using NetCord.Services;
using NetCord.Services.ApplicationCommands;

namespace Bot.Handler;

public class BotException(string message, bool deferred = false, Exception? innerException = null) : Exception(message, innerException)
{
    public bool Deferred { get; } = deferred;
}

public class ErrorCommandResultHandler<TContext> : IApplicationCommandResultHandler<TContext>
    where TContext : IApplicationCommandContext
{
    public static ErrorCommandResultHandler<TContext> Default => new();

    public static ErrorCommandResultHandler<TContext> Ephemeral => new EphemeralApplicationCommandResultHandler();

    protected ErrorCommandResultHandler()
    {
    }

    private sealed class EphemeralApplicationCommandResultHandler : ErrorCommandResultHandler<TContext>
    {
        public override ValueTask<InteractionMessageProperties> GetFailMessageAsync(IFailResult failResult, TContext context, IServiceProvider services)
        {
            return new(new InteractionMessageProperties
            {
                Content = failResult.Message,
                Flags = MessageFlags.Ephemeral,
            });
        }
    }

    public async ValueTask HandleResultAsync(IExecutionResult result, TContext context, GatewayClient? client, ILogger logger, IServiceProvider services)
    {
        if (result is not IFailResult failResult)
            return;

        var interaction = context.Interaction;

        var commandName = interaction.Data.Name;

        var isDeferred = false;

        if (failResult is IExceptionResult exceptionResult)
        {
            logger.LogError(exceptionResult.Exception, "Execution of an application command of name '{Name}' failed with an exception", commandName);
            if (exceptionResult.Exception is BotException botException)
            {
                isDeferred = botException.Deferred;
            }
        }
        else
        {
            logger.LogDebug("Execution of an application command of name '{Name}' failed with '{Message}'", commandName, failResult.Message);
        }
            
        var response = await GetFailMessageAsync(failResult, context, services).ConfigureAwait(false);

        if (isDeferred)
        {
            await interaction.ModifyResponseAsync(message => message.AddEmbeds(response.Embeds)).ConfigureAwait(false);
        }

        await interaction.SendResponseAsync(InteractionCallback.Message(response)).ConfigureAwait(false);
    }

    public virtual ValueTask<InteractionMessageProperties> GetFailMessageAsync(IFailResult failResult, TContext context, IServiceProvider services)
    {
        var embed = EmbedBuilder.Res(failResult.Message, 0xED4245);

        return new(new InteractionMessageProperties
        {
            Embeds = [embed],
            Flags = MessageFlags.Ephemeral,
        });
    }
}