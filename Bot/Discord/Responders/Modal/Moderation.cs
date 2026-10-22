using Bangboo.Utils;
using Bot.Handler;
using Bot.Utils.Constants;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;

namespace Bot.Discord.Responders.Modal;

public class ModerationResponderModule(IOptions<Colors> colors) : ComponentInteractionModule<ModalInteractionContext>
{
    // Limite do bulk delete do Discord (2-100 mensagens por chamada)
    private const int MaxDeleteLimit = 100;
    // Discord só permite timeout de até 28 dias
    private const double MaxTimeoutSeconds = 28 * 24 * 60 * 60;
    // Bulk delete só funciona com mensagens de até 14 dias
    private static readonly TimeSpan BulkDeleteMaxAge = TimeSpan.FromDays(14);

    // ---------------------------------------------------------------- DELETE MESSAGE
    [ComponentInteraction("modal/moderate/delete-message")]
    public async Task DeleteMessage()
    {
        var selectedUsers = GetUserMenu("delete-message-users")?.SelectedValues;
        var limitText = GetTextInput("delete-message-limit");

        if (selectedUsers is null || !selectedUsers.Any())
        {
            throw new BotException("Failed to get selected users input field values");
        }

        if (!int.TryParse(limitText, out var limit) || limit < 1 || limit > MaxDeleteLimit)
        {
            throw new BotException($"The limit must be a number between 1 and {MaxDeleteLimit}");
        }

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Ephemeral));

        var selectedIds = selectedUsers.Select(u => u.Id).ToHashSet();
        var channel = Context.Interaction.Channel;

        // Take(limit) é essencial: BatchSize é só o tamanho da página, sem o Take
        // o enumerador continuaria buscando TODAS as mensagens do canal.
        var messages = await channel
            .GetMessagesAsync(new() { BatchSize = limit })
            .Take(limit)
            .ToListAsync();

        var cutoff = DateTimeOffset.UtcNow - BulkDeleteMaxAge;
        var toDelete = messages
            .Where(m => selectedIds.Contains(m.Author.Id) && m.CreatedAt > cutoff)
            .ToList();

        var failedAuthors = new HashSet<ulong>();
        foreach (var chunk in toDelete.Chunk(100))
        {
            if (chunk.Length == 1)
                await channel.DeleteMessageAsync(chunk[0].Id);
            else
                await channel.DeleteMessagesAsync(chunk.Select(m => m.Id));
        }

        var deletedByUser = toDelete
            .Where(m => !failedAuthors.Contains(m.Author.Id))
            .GroupBy(m => m.Author.Id)
            .ToDictionary(g => g.Key, g => g.Count());

        var successList = new List<string>();
        var failList = new List<string>();
        foreach (var id in selectedIds)
        {
            if (failedAuthors.Contains(id))
                failList.Add($"<@{id}>");
            else if (deletedByUser.TryGetValue(id, out var count))
                successList.Add($"<@{id}> ({count})");
            else
                failList.Add($"<@{id}> (nothing to delete)");
        }

        await Respond(EmbedBuilder.OfficerCuiAction(Context.User, "Delete message", successList, failList));
    }

    // ---------------------------------------------------------------- TIMEOUT
    [ComponentInteraction("modal/moderate/timeout")]
    public async Task Timeout()
    {
        var selectedUsers = GetUserMenu("timeout-users")?.SelectedValues;
        var timeout = GetStringMenu("timeout-duration")?.SelectedValues.FirstOrDefault();
        var reason = GetTextInput("timeout-reason");

        if (selectedUsers is null || !selectedUsers.Any() || timeout is null)
        {
            throw new BotException("Failed to get input field values");
        }

        // Valida ANTES de dar defer: depois do defer não dá mais para usar SendResponseAsync
        if (!double.TryParse(timeout, out var timeoutSeconds) || timeoutSeconds <= 0 || timeoutSeconds > MaxTimeoutSeconds)
        {
            await Fail("Failed to convert timeout value to seconds (max 28 days)");
            return;
        }

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Ephemeral));

        var timeoutUntil = DateTimeOffset.UtcNow.AddSeconds(timeoutSeconds);
        var (successList, failList) = await RunPerUser(
            selectedUsers.Select(u => u.Id),
            skipOwner: true,
            id => Context.Guild.ModifyUserAsync(
                id,
                properties => properties.WithTimeOutUntil(timeoutUntil),
                new RestRequestProperties { AuditLogReason = AuditReason(reason, "timeout") }));

        await Respond(EmbedBuilder.OfficerCuiAction(Context.User, "Timeout", successList, failList, reason));
    }

    // ---------------------------------------------------------------- KICK
    [ComponentInteraction("modal/moderate/kick")]
    public async Task Kick()
    {
        var selectedUsers = GetUserMenu("kick-users")?.SelectedValues;
        var reason = GetTextInput("kick-reason");

        if (selectedUsers is null || !selectedUsers.Any())
        {
            throw new BotException("Failed to get selected users input field values");
        }

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Ephemeral));

        var (successList, failList) = await RunPerUser(
            selectedUsers.Select(u => u.Id),
            skipOwner: true,
            id => Context.Guild.KickUserAsync(id, new() { AuditLogReason = AuditReason(reason, "kick") }));

        await Respond(EmbedBuilder.OfficerCuiAction(Context.User, "Kick", successList, failList, reason));
    }

    // ---------------------------------------------------------------- BAN
    [ComponentInteraction("modal/moderate/ban")]
    public async Task Ban()
    {
        var selectedUsers = GetUserMenu("ban-users")?.SelectedValues;
        var reason = GetTextInput("ban-reason");

        if (selectedUsers is null || !selectedUsers.Any())
        {
            throw new BotException("Failed to get selected users input field values");
        }

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Ephemeral));

        // Ban individual: o bulk ban marcava todo mundo como sucesso/falha de uma vez
        var (successList, failList) = await RunPerUser(
            selectedUsers.Select(u => u.Id),
            skipOwner: true,
            id => Context.Guild.BanUserAsync(id, 604800, new() { AuditLogReason = AuditReason(reason, "ban") }));

        await Respond(EmbedBuilder.OfficerCuiAction(Context.User, "Ban", successList, failList, reason));
    }

    // ---------------------------------------------------------------- UNBAN
    [ComponentInteraction("modal/moderate/unban")]
    public async Task Unban()
    {
        var selectedUsers = GetUserMenu("unban-users")?.SelectedValues;

        if (selectedUsers is null || !selectedUsers.Any())
        {
            throw new BotException("Failed to get selected users input field values");
        }

        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Ephemeral));

        var (successList, failList) = await RunPerUser(
            selectedUsers.Select(u => u.Id),
            skipOwner: false,
            id => Context.Guild.UnbanUserAsync(id));

        await Respond(EmbedBuilder.OfficerCuiAction(Context.User, "Unban", successList, failList));
    }

    // ---------------------------------------------------------------- HELPERS
    private IEnumerable<T> Inputs<T>() =>
        Context.Components.OfType<Label>().Select(l => l.Component).OfType<T>();

    private UserMenu? GetUserMenu(string customId) =>
        Inputs<UserMenu>().FirstOrDefault(c => c.CustomId == customId);

    private StringMenu? GetStringMenu(string customId) =>
        Inputs<StringMenu>().FirstOrDefault(c => c.CustomId == customId);

    private string? GetTextInput(string customId) =>
        Inputs<TextInput>().FirstOrDefault(c => c.CustomId == customId)?.Value;

    // Resposta de erro ANTES do defer (efêmera)
    private Task Fail(string message) =>
        Context.Interaction.SendResponseAsync(InteractionCallback.Message(new()
        {
            Embeds = [EmbedBuilder.Res(message, colors.Value.Danger)],
            Flags = MessageFlags.Ephemeral
        }));

    // Resposta final DEPOIS do defer (já é efêmera, não precisa/pode mudar flags)
    private Task Respond(EmbedProperties embed) =>
        Context.Interaction.ModifyResponseAsync(message => message.AddEmbeds([embed]));

    // Antes: "$${...}" imprimia um "$" literal no audit log
    private string AuditReason(string? reason, string action)
    {
        var text = string.IsNullOrWhiteSpace(reason)
            ? $"{Context.User.GlobalName ?? Context.User.Username} apply {action} via Bangboo"
            : reason;

        // Audit log reason aceita no máximo 512 caracteres
        return text.Length > 512 ? text[..512] : text;
    }

    private async Task<(List<string> Success, List<string> Fail)> RunPerUser(
        IEnumerable<ulong> userIds,
        bool skipOwner,
        Func<ulong, Task> action)
    {
        var success = new List<string>();
        var fail = new List<string>();

        foreach (var id in userIds.Distinct())
        {
            // O dono do servidor era ignorado silenciosamente; agora aparece como falha
            if (skipOwner && id == Context.Guild.OwnerId)
            {
                fail.Add($"<@{id}>");
                continue;
            }

            try
            {
                await action(id);
                success.Add($"<@{id}>");
            }
            catch (RestException)
            {
                fail.Add($"<@{id}>");
            }
        }

        return (success, fail);
    }
}