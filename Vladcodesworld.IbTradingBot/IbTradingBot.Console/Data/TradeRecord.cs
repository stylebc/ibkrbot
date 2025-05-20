namespace IbTradingBot.Console.Data;

using System;
using System.ComponentModel.DataAnnotations;

public class TradeRecord
{
    [Key]
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty; // stored as "Buy" or "Sell"

    public double Price { get; set; }
}
