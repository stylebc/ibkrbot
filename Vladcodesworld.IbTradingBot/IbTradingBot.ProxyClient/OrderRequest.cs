namespace IbTradingBot.ProxyClient;

public class OrderRequest
{
    public string Symbol { get; set; }
    public string Side { get; set; }
    public int Quantity { get; set; }
    public string OrderType { get; set; } = "MKT";
    public double? LimitPrice { get; set; }
}   