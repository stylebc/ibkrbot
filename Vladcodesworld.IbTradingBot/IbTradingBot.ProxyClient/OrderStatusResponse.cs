namespace IbTradingBot.ProxyClient;

public class OrderStatusResponse
{
    public int OrderId { get; set; }
    public string Status { get; set; }
    public double Filled { get; set; }
    public double AvgFillPrice { get; set; }
    public string Message { get; set; }
}