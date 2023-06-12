using System.Text.Json;

using app.Bots;
using app.Contexts;
using app.Helpers;

using Microsoft.AspNetCore.Mvc;

using Telegram.Bot.Types;

namespace app.Controllers;

[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
    private readonly TelegramBot _bot;

    private readonly PostgresContext _context;

    public BotController(TelegramBot bot, PostgresContext context)
    {
        _bot = bot;
        _context = context;
    }

    [HttpPost("callback")]
    public ActionResult Callback([FromBody] JsonElement update)
    {
        Console.WriteLine(JsonSerializer.Serialize(update));
        return Ok();
    }

    [HttpGet("get_user_start_link")]
    public async Task<ActionResult<string>> GetUserStartLinkAsync([FromQuery] int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null) return NotFound();

        var base64UserId = Base64Helper.GetBase64String(userId);
        var botUsername = await _bot.GetBotUsername();
        var link = $"https://t.me/{botUsername}?start={base64UserId}";
        return Ok(link);
    }

    [HttpPost("set_webhook")]
    public async Task<ActionResult> SetWebhookAsync(string url)
    {
        await _bot.SetBotWebhook(url);
        return Ok();
    }


    [HttpPost("send_message")]
    public async Task<IActionResult> SendMessageAsync(long chatId, string text)
    {
        await _bot.SendMessageAsync(chatId, text);
        return Ok();
    }
}

