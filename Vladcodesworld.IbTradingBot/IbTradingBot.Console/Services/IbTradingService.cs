using IbTradingBot.Console.Enums;
using IbTradingBot.Console.Interfaces;
using IbTradingBot.ProxyClient;

namespace IbTradingBot.Console.Services;

using System;
using System.Linq;
using System.Threading.Tasks;

public class IbTradingService
{
    private readonly IbApiClient _client;
    private readonly IBotQueryService _queryService;
    private readonly IBotCommandService _commandService;
    private readonly string _symbol;
    private readonly string _barSize;
    private readonly string _duration;
    private readonly int _orderQuantity;

    public IbTradingService(IbApiClient client, IBotQueryService queryService, IBotCommandService commandService, string symbol = "AAPL", string barSize = "1 day", string duration = "3 M", int orderQuantity = 10)
    {
        _client = client;
        _queryService = queryService;
        _commandService = commandService;
        _symbol = symbol;
        _barSize = barSize;
        _duration = duration;
        _orderQuantity = orderQuantity;
    }

    public async Task RunStrategyAsync()
    {
        Console.WriteLine($"📊 Loading historical data for {_symbol}...");
        var bars = await _client.GetHistoricalAsync(_symbol, _barSize, _duration);

        if (bars.Count < 50)
        {
            Console.WriteLine("⚠️ Not enough data for strategy.");
            return;
        }

        var closes = bars.Select(b => b.Close).ToList();
        var smaFast = closes.Skip(closes.Count - 10).Average();
        var smaSlow = closes.Skip(closes.Count - 50).Average();

        Console.WriteLine($"Fast SMA: {smaFast}, Slow SMA: {smaSlow}");

        TradeSignal signal = TradeSignal.None;
        if (smaFast > smaSlow) signal = TradeSignal.Buy;
        else if (smaFast < smaSlow) signal = TradeSignal.Sell;

        if (signal == TradeSignal.None)
        {
            Console.WriteLine("ℹ️ No trade signal generated.");
            return;
        }

        var lastSignal = _queryService.GetLastSignal(_symbol);
        if (signal == lastSignal)
        {
            Console.WriteLine($"↩️ Same signal as last time ({signal}), skipping.");
            return;
        }

        Console.WriteLine($"📈 New signal: {signal} {_symbol}");

        var orderRequest = new OrderRequest
        {
            Symbol = _symbol,
            Side = signal.ToString(),
            Quantity = _orderQuantity,
            OrderType = "MKT"
        };

        var orderStatus = await _client.PlaceOrderAsync(orderRequest);
        Console.WriteLine($"📝 Order placed. ID: {orderStatus?.OrderId}");

        var status = await _client.GetOrderStatusAsync(orderStatus.OrderId, timeoutSeconds: 20);
        Console.WriteLine($"✅ Order status: {status.Status}, filled: {status.Filled} @ {status.AvgFillPrice}");

        _commandService.SaveLastSignal(_symbol, signal);
    }
}
