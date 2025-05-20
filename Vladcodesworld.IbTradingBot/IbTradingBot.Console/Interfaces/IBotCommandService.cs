using IbTradingBot.Console.Data;
using IbTradingBot.Console.Enums;

namespace IbTradingBot.Console.Interfaces;

public interface IBotCommandService
{
    void SaveState(BotState state);
    void RecordTrade(TradeRecord trade);

    void SaveLastSignal(string symbol, TradeSignal signal);
}
