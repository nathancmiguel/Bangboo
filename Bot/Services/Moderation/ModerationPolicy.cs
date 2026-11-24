namespace Bot.Services.Moderation;

/// <summary>Quem pode usar o comando <c>/moderate</c> (regra pura, testada).</summary>
public static class ModerationPolicy
{
    /// <summary>
    /// O dono e quem tem Administrador sempre podem (ninguém se tranca fora);
    /// os demais precisam de um dos cargos de staff do <c>guild.json</c>.
    /// </summary>
    public static bool CanModerate(
        bool isOwner,
        bool isAdministrator,
        IEnumerable<ulong> memberRoleIds,
        IReadOnlySet<ulong> allowedRoleIds) =>
        isOwner || isAdministrator || memberRoleIds.Any(allowedRoleIds.Contains);
}
