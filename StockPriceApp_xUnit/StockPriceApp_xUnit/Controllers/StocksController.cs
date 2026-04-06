using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockPriceApp_xUnit.Models;


namespace StockPriceApp_xUnit.Controllers
{
    [Route("[controller]")]
    public class StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, ILogger<StocksController> logger) : Controller
    {
        private readonly TradingOptions _tradingOptions = tradingOptions.Value;
        private readonly IFinnhubService _finnhubService = finnhubService;
        private readonly ILogger<StocksController> _logger = logger;


        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            _logger.LogInformation("In StocksController.Explore() action method");
            _logger.LogDebug("stock: {stock}, showAll: {showAll}", stock, showAll);

            List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

            List<Stock> stocks = new List<Stock>();

            if (stocksDictionary is not null)
            {
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = stocksDictionary
                         .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
                         .ToList();
                    }
                }

                stocks = stocksDictionary
                 .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
                .ToList();
            }

            ViewBag.stock = stock;
            return View(stocks);
        }
    }
}

