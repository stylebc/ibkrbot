using IbTradingBot.Console.Data;
using IbTradingBot.Console.Enums;

namespace IbTradingBot.Console.Interfaces;

public interface IBotQueryService
{
    BotState LoadState();

    public TradeSignal GetLastSignal(string symbol);
}

