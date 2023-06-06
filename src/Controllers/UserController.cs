using System.Text.Json;

using app.Contexts;
using app.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly PostgresContext _context;

    public UserController(ILogger<UserController> logger, PostgresContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var userList = await _context.Users.ToListAsync();

        return Ok(userList);
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return StatusCode(201, user);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<User>> GetUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        return Ok(user);
    }


    [HttpPut("{userId}")]
    public async Task<ActionResult<User>> PutUser(int userId, UserUpdate payload)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null) return NotFound();

        user.Name = payload.Name;
        user.Email = payload.Email;
        user.Password = payload.Password;
        user.TelegramChatId = payload.TelegramChatId;
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPatch("{userId}")]
    public async Task<ActionResult<User>> PatchUser(int userId, UserPartiallyUpdate payload)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null) return NotFound();

        foreach (var property in payload.GetType().GetProperties())
        {
            // 必須使用 user 自己的 type property 來設值
            var prop = user.GetType().GetProperty(property.Name);
            if (prop is not null)
            {
                var value = property.GetValue(payload);
                // 此作法無法設定空值給 user
                if (value is not null)
                {
                    prop.SetValue(user, value);
                }
            }
        }
        await _context.SaveChangesAsync();

        return Ok(user);
    }


    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}