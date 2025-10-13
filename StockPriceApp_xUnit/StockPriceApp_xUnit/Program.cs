using Service;
using ServiceContracts;

namespace StockPriceApp_xUnit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddSingleton<IFinnhubService ,FinnhubService>();
            builder.Services.AddSingleton<IStocksService, StocksService>();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
