using System;
using System.ComponentModel.DataAnnotations;
using IbTradingBot.Console.Enums;

namespace IbTradingBot.Console.Data;
using System;
using System.ComponentModel.DataAnnotations;

public class BotState
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Symbol { get; set; }

    public bool InPosition { get; set; }

    public TradeSignal LastSignal { get; set; } = TradeSignal.None;

    public DateTime LastTradeTime { get; set; }

    public double EntryPrice { get; set; }

    public double MaxPriceSinceEntry { get; set; }
}

