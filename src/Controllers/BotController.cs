using app.Bots;

using Microsoft.AspNetCore.Mvc;


namespace app.Controllers;

public class BotController : ControllerBase
{
    private readonly TelegramBot _bot;

    public BotController(TelegramBot bot)
    {
        _bot = bot;
    }

    [HttpPost("/bot/send_message")]
    public async Task<IActionResult> SendMessageAsync(long chatId, string text)
    {
        await _bot.SendMessageAsync(chatId, text);
        return Ok();
    }
}

