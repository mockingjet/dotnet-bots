
using System.Text.Json;

using app.Bots;
using app.Contexts;
using app.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[ApiController]
[Route("line/notify")]
public class LineNotifyController : ControllerBase
{
    private readonly LineNotify _bot;
    private readonly PostgresContext _context;

    public LineNotifyController(LineNotify bot, PostgresContext context)
    {
        _bot = bot;
        _context = context;
    }

    [HttpGet("callback")]
    public async Task<ActionResult> Callback(string? code, string? state)
    {
        if (code is null || state is null) return BadRequest();
        var token = await _bot.GetTokenAsync(code);
        Console.WriteLine(token);
        // todo: bind token to user(state) data
        // todo: reply bind success to user
        return Ok(token);
    }

    [HttpGet("get_user_start_link")]
    public async Task<ActionResult<string>> GetUserStartLinkAsync([FromQuery] int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null) return NotFound();

        var base64UserId = Base64Helper.GetBase64String(userId);
        var link = _bot.GetLineNotifyAuthorizeLink(base64UserId);
        return Ok(link);
    }
}
