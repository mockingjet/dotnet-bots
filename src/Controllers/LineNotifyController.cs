
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

    public async Task<ActionResult> CallbackAsync(string? code, string? state)
    {
        if (code is null || state is null) return BadRequest("code or state is null");

        var token = await _bot.GetTokenAsync(code);
        if (token is null) return BadRequest("token is null");

        var useId = Base64Helper.GetIntValue(state);
        var user = await _context.Users.FindAsync(useId);
        if (user is null) return BadRequest("state is invalid");

        user.LineNotifyToken = token;
        await _context.SaveChangesAsync();

        var res = await _bot.SendMessageAsync(token, "綁定成功!");
        if (res is false) return BadRequest("send message failed");

        // 此處回傳內容會顯示在 Line Notify Redirect 網頁上
        // todo: 回傳HTML
        return Ok("Success");
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
