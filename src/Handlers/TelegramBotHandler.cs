using app.Bots;
using app.Contexts;
using app.Helpers;

using Microsoft.AspNetCore.Mvc;

using Telegram.Bot.Types;

namespace app.Handlers;

public class TelegramBotHandler
{
    private TelegramBot _bot;

    private PostgresContext _context;

    public TelegramBotHandler(TelegramBot bot, PostgresContext context)
    {
        _bot = bot;
        _context = context;
    }

    public async Task HandleAsync(Update update)
    {
        // todo: add handler interface and use switch case
        if (update.Message is not null)
        {
            await HandleMessageAsync(update.Message);
        }
        else
        {
            HandleUnknownAsync(update);
        }
    }

    private async Task HandleMessageAsync(Message message)
    {
        if (message.Text is null) return;

        var action = message.Text.Split(' ')[0];

        if (action == "/start")
        {
            await HandleUserStartCommandAsync(message);
        }
    }

    private async Task HandleUserStartCommandAsync(Message message)
    {
        if (message.Text is null || message.From is null) return;

        var chatId = message.Chat.Id;
        var base64UserId = message.Text.Split(' ')[1];
        var useId = Base64Helper.GetIntValue(base64UserId);

        var user = await _context.Users.FindAsync(useId);
        if (user is null) return;

        user.TelegramChatId = chatId;
        await _context.SaveChangesAsync();
        await _bot.SendMessageAsync(chatId, "綁定成功!");
    }

    private void HandleUnknownAsync(Update update)
    {
        Console.WriteLine("Unknown update payload: " + update);
    }
}