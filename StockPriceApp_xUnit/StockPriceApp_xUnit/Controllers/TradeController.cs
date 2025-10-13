using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using ServiceContracts;
using System.Globalization;

namespace StockPriceApp_xUnit.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly TradingOptions _tradeOptions;
        private readonly IConfiguration _configuration;

        public TradeController(IStocksService stocksService, IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration)
        {
            _stocksService = stocksService;
            _finnhubService = finnhubService;
            _tradeOptions = tradingOptions.Value;
            _configuration = configuration;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(_tradeOptions.DefaultStockSymbol))
                _tradeOptions.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? stockQuoteDictionary = await _finnhubService.GetStockPriceQuote(_tradeOptions.DefaultStockSymbol);

            Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfile(_tradeOptions.DefaultStockSymbol);

            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradeOptions.DefaultStockSymbol };

            if(companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() 
                { 
                    StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), 
                    StockName = Convert.ToString(companyProfileDictionary["name"]), 
                    Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString(), CultureInfo.InvariantCulture) 
                };
            }

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}
