using Telegram.Bot;

namespace app.Bots;

public class TelegramBot
{
    private readonly TelegramBotClient _client;

    public TelegramBot()
    {
        var TELEGRAM_BOT_TOKEN = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
        Console.WriteLine($"TELEGRAM_BOT_TOKEN: {TELEGRAM_BOT_TOKEN}");
        _client = new TelegramBotClient(TELEGRAM_BOT_TOKEN!);
    }

    public async Task SendMessageAsync(long chatId, string message)
    {
        await _client.SendTextMessageAsync(chatId, message);
    }
}
