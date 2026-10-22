using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Bot.Utils.Constants;

public class Fab
{
    [ConfigurationKeyName("engine_user_setings")]
    public FabProduct EngineUserSetings { get; set; } = new();

    [ConfigurationKeyName("internet_protocol")]
    public FabProduct InternetProtocol { get; set; } = new();
}

public class FabProduct
{
    [ConfigurationKeyName("product_name")]
    public string ProductName { get; set; } = string.Empty;

    [ConfigurationKeyName("product_desc")]
    public string ProductDesc { get; set; } = string.Empty;

    [ConfigurationKeyName("thumb_link")]
    public string ThumbLink { get; set; } = string.Empty;

    [ConfigurationKeyName("product_url")]
    public string ProductUrl { get; set; } = string.Empty;

    [ConfigurationKeyName("doc_url")]
    public string DocUrl { get; set; } = string.Empty;
}
