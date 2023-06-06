using app.Models;

using Microsoft.EntityFrameworkCore;

namespace app.Contexts;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var POSTGRES_HOST = "db";
        var POSTGRES_PORT = 5432;
        var POSTGRES_USER = Environment.GetEnvironmentVariable("POSTGRES_USER");
        var POSTGRES_PASSWORD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        var POSTGRES_DB = Environment.GetEnvironmentVariable("POSTGRES_DB");
        String POSTGRES_CONNECTION_STRING = $"Host={POSTGRES_HOST};Port={POSTGRES_PORT};Username={POSTGRES_USER};Password={POSTGRES_PASSWORD};Database={POSTGRES_DB};";
        optionsBuilder.UseNpgsql(POSTGRES_CONNECTION_STRING);
    }

    public DbSet<User> Users { get; set; }
}