using Bot.Utils.Constants;
using Bot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Gateway;
using Services;
using NetCord.Hosting.Services.ComponentInteractions;
using NetCord;
using NetCord.Services.ComponentInteractions;
using Bot.Handler;
using NetCord.Services.ApplicationCommands;
using Newtonsoft.Json;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("Data/guild.json", optional: true, reloadOnChange: true)
    .AddJsonFile("Data/colors.json", optional: false, reloadOnChange: true)
    .AddJsonFile("Data/emojis.json", optional: false, reloadOnChange: true)
    .AddJsonFile("Data/fab.json", optional: true, reloadOnChange: true)
    .AddJsonFile("Data/social.json", optional: true, reloadOnChange: true);

builder.Services.Configure<Colors>(builder.Configuration.GetSection("colors"));
builder.Services.Configure<Emojis>(emojis =>
{
    var emojisPath = Path.Combine(AppContext.BaseDirectory, "Data", "emojis.json");
    JsonConvert.PopulateObject(File.ReadAllText(emojisPath), emojis);
});
builder.Services.Configure<Channels>(builder.Configuration.GetSection("channels"));
builder.Services.Configure<Roles>(builder.Configuration.GetSection("roles"));
builder.Services.Configure<Fab>(builder.Configuration);
builder.Services.Configure<Socials>(builder.Configuration.GetSection("socials"));

builder.Services
    .AddDiscordGateway(options =>
    {
        options.Token = builder.Configuration["Discord:Token"];
        options.Intents = GatewayIntents.All;
    })
    .AddHttpClient()
    .AddApplicationCommands((options, services) =>
    {
        var cmdHandler = (ErrorCommandResultHandler<ApplicationCommandContext>)
            Activator.CreateInstance(
                typeof(ErrorCommandResultHandler<ApplicationCommandContext>),
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic,
                binder: null,
                args: null,
                culture: null)!;;
        options.ResultHandler = cmdHandler;
    })
    .AddGatewayHandlers(typeof(Program).Assembly);

builder.Services
    .AddComponentInteractions<ModalInteraction, ModalInteractionContext>();

builder.Services
    .AddSingleton<IAutomodService, AutomodService>()
    .AddSingleton<IGlobalService, GlobalService>();

var host = builder.Build();

host.AddModules(typeof(Program).Assembly);

await host.RunAsync();