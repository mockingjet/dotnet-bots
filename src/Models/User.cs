namespace app.Models;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public long? TelegramChatId { get; set; }

    public string? LineNotifyToken { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

public class UserUpdate
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public long? TelegramChatId { get; set; }

    public string? LineNotifyToken { get; set; }
}

public class UserPartiallyUpdate
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public long? TelegramChatId { get; set; }

    public string? LineNotifyToken { get; set; }
}