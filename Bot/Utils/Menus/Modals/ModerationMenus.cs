using NetCord.Rest;

namespace Bot.Utils.Menus.Modals;

public class ModerationMenus
{
    public static ModalProperties DeleteMessageMenu()
    {
        var deleteMessageModal = new ModalProperties("modal/moderate/delete-message", "Delete messages")
            .AddComponents(
                new TextDisplayProperties("# Warning\n" + "Bangboo will automatically filter and remove the guild owner and moderators if any are selected."),
                
                new LabelProperties("User(s)", new UserMenuProperties("delete-message-users")
                {
                    Placeholder = "Select at least one user",
                    MinValues = 1,
                    MaxValues = 10
                }),

                new LabelProperties("Limit", new TextInputProperties("delete-message-limit", TextInputStyle.Short)
                {
                    Placeholder = "Numbers from 1 to 100",
                    MinLength = 1,
                    MaxLength = 3,
                    Required = true,
                    Value = "1"
                })
            );
        return deleteMessageModal;
    }
    
    public static ModalProperties TimeoutMenu()
    {
        var timeoutModal = new ModalProperties("modal/moderate/timeout", "Timeout user(s)")
            .AddComponents(
                new TextDisplayProperties("# Warning\nBangboo will automatically filter and remove the guild owner and moderators if any are selected."),
                

                new LabelProperties("User(s)", new UserMenuProperties("timeout-users")
                    .WithPlaceholder("Select at least one user")
                    .WithMinValues(1)
                    .WithMaxValues(10)
                ),
                
                new LabelProperties("Duration", new StringMenuProperties("timeout-duration", [
                        new StringMenuSelectOptionProperties("60 seconds", "60"),
                        new StringMenuSelectOptionProperties("5 minutes", "300"),
                        new StringMenuSelectOptionProperties("10 minutes", "600"),
                        new StringMenuSelectOptionProperties("1 hour", "3600"),
                        new StringMenuSelectOptionProperties("1 day", "86400")
                    ])
                    .WithMinValues(1)
                    .WithPlaceholder("TImeout duration")
                ),

                new LabelProperties("Reason", new TextInputProperties("timeout-reason", TextInputStyle.Short)
                    .WithPlaceholder("Visible in audity logs")
                    .WithRequired(false)
                )
            );
        return timeoutModal;
    }
    
    public static ModalProperties KickMenu()
    {
        var kickModal = new ModalProperties("modal/moderate/kick", "Kick user(s)")
            .AddComponents(
                new TextDisplayProperties("# Warning\n" + "Bangboo will automatically filter and remove the guild owner and moderators if any are selected."),
                
                new LabelProperties("User(s)", new UserMenuProperties("kick-users")
                        .WithPlaceholder("Select at least one user")
                    .WithMinValues(1)
                    .WithMaxValues(10)
                ),

                new LabelProperties("Reason", new TextInputProperties("kick-reason", TextInputStyle.Short)
                        .WithPlaceholder("Visible in auditlogs channel")
                        .WithRequired(false)
                )
            );
        return kickModal;
    }
    
    public static ModalProperties BanMenu()
    {
        var banModal = new ModalProperties("modal/moderate/ban", "Ban user(s)")
            .AddComponents(
                new TextDisplayProperties("# Warning\n" + "Bangboo will automatically filter and remove the guild owner and moderators if any are selected."),
                
                new LabelProperties("User(s)", new UserMenuProperties("ban-users")
                        .WithPlaceholder("Select at least one user")
                        .WithMinValues(1)
                        .WithMaxValues(10)
                ),

                new LabelProperties("Reason", new TextInputProperties("kick-reason", TextInputStyle.Short)
                    .WithPlaceholder("Visible in auditlogs channel")
                    .WithRequired(false)
                )
            );
        return banModal;
    }
    
    public static ModalProperties UnbanMenu(List<GuildBan> guildBans)
    {
        var selectMenu = new StringMenuProperties("unban-users")
        {
            Placeholder = "Select at least one user",
            MinValues = 1,
            MaxValues = guildBans.Count > 10 ? 10 : null,
        };
        guildBans.ForEach(g =>
        {
            var opt = new StringMenuSelectOptionProperties(g.User.GlobalName ?? g.User.Username, g.User.Id.ToString())
                .WithDescription(g.Reason);
            selectMenu.AddOptions(opt);
        });
        var unbanModal = new ModalProperties("modal/moderate/unban", "Unban user(s)")
            .AddComponents(
                new LabelProperties("User(s)", selectMenu)
            );
        return unbanModal;
    }
}