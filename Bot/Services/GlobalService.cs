using Bot.Handler;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCord;
using NetCord.Rest;
using SkiaSharp;

namespace Bot.Services;

public enum EMemberEvent
{
    Join,
    Boost,
    Leave,
    Mod
}

public interface IGlobalService
{
    Task<SKData> GlobalMessage(User user, EMemberEvent eventType = EMemberEvent.Join);
}

public class GlobalService : IGlobalService, IDisposable
{
    private const int CanvasWidth = 2800;
    private const int CanvasHeight = 560;
    private const int AvatarSize = 400;
    private const float AvatarX = 5;
    private const float AvatarY = 160;

    private readonly SKBitmap _defaultAvatar;
    private readonly SKBitmap _newBackground;
    private readonly SKBitmap _boostBackground;
    private readonly SKBitmap _modBackground;
    private readonly SKBitmap _leftBackground;
    private readonly SKFont _fredokaFont;
    private readonly SKFont _robotoFont;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public GlobalService(ILogger<GlobalService> logger, IHttpClientFactory httpClientFactory)
    {
        _defaultAvatar = LoadBitmap("Assets/member/default_avatar.png");
        _newBackground = LoadBitmap("Assets/cards/card-new.png");
        _boostBackground = LoadBitmap("Assets/cards/card-boost.png");
        _modBackground = LoadBitmap("Assets/cards/card-mod.png");
        _leftBackground = LoadBitmap("Assets/cards/card-left.png");

        _fredokaFont = LoadFont("Assets/fonts/Fredoka-Medium.ttf", 200);
        _robotoFont = LoadFont("Assets/fonts/Roboto-Medium.ttf", 96);
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    public async Task<SKData> GlobalMessage(User user, EMemberEvent eventType = EMemberEvent.Join)
    {
        // Avatar já vem recortado e redimensionado para 400x400
        using var avatar = await LoadAvatarAsync(user);
        var sampling = new SKSamplingOptions(SKCubicResampler.Mitchell);

        using var bitmap = new SKBitmap(new SKImageInfo(CanvasWidth, CanvasHeight, SKColorType.Rgba8888, SKAlphaType.Premul));
        using var canvas = new SKCanvas(bitmap);

        var background = eventType switch
        {
            EMemberEvent.Join => _newBackground,
            EMemberEvent.Leave => _leftBackground,
            EMemberEvent.Boost => _boostBackground,
            _ => _modBackground
        };
        canvas.DrawBitmap(background, 0, 0, sampling);

        // Avatar circular: desenha um círculo usando o avatar como shader (sem ClipPath)
        const float avatarY = AvatarY;

        using (var avatarImage = SKImage.FromBitmap(avatar))
        using (var avatarShader = avatarImage.ToShader(
                   SKShaderTileMode.Clamp,
                   SKShaderTileMode.Clamp,
                   sampling,
                   SKMatrix.CreateTranslation(AvatarX, avatarY)))
        using (var avatarPaint = new SKPaint { IsAntialias = true, Shader = avatarShader })
        {
            canvas.DrawCircle(
                AvatarX + AvatarSize / 2f,
                avatarY + AvatarSize / 2f,
                AvatarSize / 2f,
                avatarPaint);
        }

        var globalName = user.GlobalName ?? user.Username;
        var username = user.Username;

        using (var textPaint = new SKPaint { IsAntialias = true, Color = SKColors.White })
        {
            canvas.DrawText(globalName, new SKPoint(530, 297), SKTextAlign.Left, _fredokaFont, textPaint);
            canvas.DrawText($"@{username}", new SKPoint(530, 452), SKTextAlign.Left, _robotoFont, textPaint);
        }

        canvas.Flush();

        using var image = SKImage.FromBitmap(bitmap);
        return image.Encode(SKEncodedImageFormat.Png, 90);
    }

    private async Task<SKBitmap> LoadAvatarAsync(User user)
    {
        var avatarUrl = user.GetAvatarUrl(ImageFormat.Png);
        if (avatarUrl is null)
            return ResizeAvatar(_defaultAvatar);

        try
        {
            // Baixa TODOS os bytes antes de decodificar.
            // Decodificar direto do stream de rede pode gerar imagem incompleta
            // (só o topo aparece, o resto fica transparente).
            var bytes = await _httpClient.GetByteArrayAsync(avatarUrl.ToString());

            using var downloadedAvatar = SKBitmap.Decode(bytes);
            return downloadedAvatar is null ? ResizeAvatar(_defaultAvatar) : ResizeAvatar(downloadedAvatar);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            _logger.LogError(exception, "Failed to get avatar for user {UserId} ({Username})", user.Id, user.Username);
            return ResizeAvatar(_defaultAvatar);
        }
    }

    private static SKBitmap ResizeAvatar(SKBitmap avatar)
    {
        var size = Math.Min(avatar.Width, avatar.Height);
        var x = (avatar.Width - size) / 2f;
        var y = (avatar.Height - size) / 2f;
        var sourceRect = new SKRect(x, y, x + size, y + size);

        // IMPORTANTE: Premul (o Skia não desenha corretamente em bitmaps Unpremul)
        var resizedAvatar = new SKBitmap(new SKImageInfo(AvatarSize, AvatarSize, SKColorType.Rgba8888, SKAlphaType.Premul));

        using (var canvas = new SKCanvas(resizedAvatar))
        {
            canvas.Clear(SKColors.Transparent);
            canvas.DrawBitmap(
                avatar,
                sourceRect,
                new SKRect(0, 0, AvatarSize, AvatarSize),
                new SKSamplingOptions(SKCubicResampler.Mitchell));
            canvas.Flush();
        }

        return resizedAvatar;
    }

    private static SKBitmap LoadBitmap(string relativePath)
    {
        return SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, relativePath))
            ?? throw new BotException($"Failed to load image asset '{relativePath}'.");
    }

    private static SKFont LoadFont(string relativePath, float size)
    {
        using var stream = File.OpenRead(Path.Combine(AppContext.BaseDirectory, relativePath));
        var typeface = SKTypeface.FromStream(stream)
            ?? throw new BotException($"Failed to load font asset '{relativePath}'.");
        return new SKFont(typeface, size);
    }

    public void Dispose()
    {
        _defaultAvatar.Dispose();
        _newBackground.Dispose();
        _modBackground.Dispose();
        _leftBackground.Dispose();
        _fredokaFont.Dispose();
        _robotoFont.Dispose();
    }
}