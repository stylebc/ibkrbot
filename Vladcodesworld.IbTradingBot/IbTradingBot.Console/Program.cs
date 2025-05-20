using IbTradingBot.Console.Data;
using IbTradingBot.Console.Interfaces;
using IbTradingBot.Console.Models;
using IbTradingBot.Console.Repositories;
using IbTradingBot.Console.Services;
using IbTradingBot.ProxyClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.Configure<TradingBotOptions>(
            builder.Configuration.GetSection("TradingBotOptions"));

        builder.Services.AddDbContext<BotDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IBotQueryService, BotQueryService>();
        builder.Services.AddScoped<IBotCommandService, BotCommandService>();
        builder.Services.AddSingleton<IbTradingService>();

        builder.Services.AddHttpClient<IbApiClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:8000/");
        });

        var app = builder.Build();
        var bot = app.Services.GetRequiredService<IbTradingService>();

        await bot.RunStrategyAsync();
    }
}