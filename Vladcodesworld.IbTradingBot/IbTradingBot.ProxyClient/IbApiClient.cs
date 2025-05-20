using System.Net.Http.Json;

namespace IbTradingBot.ProxyClient;

public class IbApiClient
{
    private readonly HttpClient _http;

    public IbApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PriceBar>> GetHistoricalAsync(string symbol, string barSize = "1 day", string duration = "1 M")
    {
        var uri = $"historical?symbol={symbol}&barSize={barSize}&duration={duration}";
        return await _http.GetFromJsonAsync<List<PriceBar>>(uri) ?? new();
    }

    public async Task<OrderStatusResponse?> PlaceOrderAsync(OrderRequest request)
    {
        var response = await _http.PostAsJsonAsync("place-order", request);
        var result = await response.Content.ReadFromJsonAsync<OrderStatusResponse>();
        return result;
    }

    public async Task<OrderStatusResponse> GetOrderStatusAsync(int orderId, int timeoutSeconds = 10)
    {
        var uri = $"order-status?orderId={orderId}&timeout={timeoutSeconds}";
        return await _http.GetFromJsonAsync<OrderStatusResponse>(uri);
    }
}
