using Telegram.Bot;

using TGTypes = Telegram.Bot.Types;

namespace app.Bots;

public class TelegramBot
{
    private readonly TelegramBotClient _client;

    public TelegramBot()
    {
        var TELEGRAM_BOT_TOKEN = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
        _client = new TelegramBotClient(TELEGRAM_BOT_TOKEN!);
    }

    public async Task SendMessageAsync(long chatId, string message)
    {
        await _client.SendTextMessageAsync(chatId, message);
    }

    public async Task<TGTypes.User> GetBotInfo()
    {
        var info = await _client.GetMeAsync();
        return info;
    }

    public async Task<string> GetBotUsername()
    {
        var info = await GetBotInfo();
        return info.Username!;
    }

    public async Task SetBotWebhook(string url)
    {
        await _client.SetWebhookAsync(url);
        Console.WriteLine("Set Telegram bot webhook: " + url);
    }
}
