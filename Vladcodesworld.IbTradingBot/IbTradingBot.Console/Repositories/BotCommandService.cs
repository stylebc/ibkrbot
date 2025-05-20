using IbTradingBot.Console.Data;
using IbTradingBot.Console.Enums;
using IbTradingBot.Console.Interfaces;

namespace IbTradingBot.Console.Repositories;

public class BotCommandService : IBotCommandService
{
    private readonly BotDbContext _context;

    public BotCommandService(BotDbContext context)
    {
        _context = context;
    }

    public void SaveState(BotState state)
    {
        var existing = _context.BotStates.Find(1);
        if (existing == null)
            _context.BotStates.Add(state);
        else
            _context.Entry(existing).CurrentValues.SetValues(state);

        _context.SaveChanges();
    }

    public void RecordTrade(TradeRecord trade)
    {
        _context.Trades.Add(trade);
        _context.SaveChanges();
    }
    
    public void SaveLastSignal(string symbol, TradeSignal signal)
    {
        var entity = _context.BotStates.FirstOrDefault(s => s.Symbol == symbol);
        if (entity == null)
        {
            entity = new BotState
            {
                Symbol = symbol,
                LastSignal = signal,
                LastTradeTime = DateTime.UtcNow
            };
            _context.BotStates.Add(entity);
        }
        else
        {
            entity.LastSignal = signal;
            entity.LastTradeTime = DateTime.UtcNow;
            _context.BotStates.Update(entity);
        }

        _context.SaveChanges();
    }
}
