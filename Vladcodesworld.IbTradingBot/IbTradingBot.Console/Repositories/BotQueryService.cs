using IbTradingBot.Console.Data;
using IbTradingBot.Console.Enums;
using IbTradingBot.Console.Interfaces;

namespace IbTradingBot.Console.Repositories;

public class BotQueryService : IBotQueryService
{
    private readonly BotDbContext _context;

    public BotQueryService(BotDbContext context)
    {
        _context = context;
    }

    public BotState LoadState()
    {
        return _context.BotStates.FirstOrDefault() ?? new BotState();
    }

    public TradeSignal GetLastSignal(string symbol)
    {
        return _context.BotStates
            .Where(s => s.Symbol == symbol)
            .Select(s => s.LastSignal)
            .FirstOrDefault();
    }
}

