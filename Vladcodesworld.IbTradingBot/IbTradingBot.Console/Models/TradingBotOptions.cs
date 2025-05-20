namespace IbTradingBot.Console.Models;

public class TradingBotOptions
{
    public ConnectionSettings Connection { get; set; } = new();
    public int ShortPeriod { get; set; }
    public int LongPeriod { get; set; }
    public double TrailingStopPercent { get; set; }
    public TimeSpan TradeCooldown { get; set; }
}
