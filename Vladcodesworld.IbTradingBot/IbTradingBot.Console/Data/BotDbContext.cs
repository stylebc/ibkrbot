using Microsoft.EntityFrameworkCore;

namespace IbTradingBot.Console.Data;

public class BotDbContext : DbContext
{
    public DbSet<BotState> BotStates { get; set; }
    public DbSet<TradeRecord> Trades { get; set; }

    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }
}
