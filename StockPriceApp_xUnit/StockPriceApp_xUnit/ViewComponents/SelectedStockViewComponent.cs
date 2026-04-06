using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockPriceApp_xUnit;

namespace StockPriceApp_xUnit.ViewComponents
{
    public class SelectedStockViewComponent(IOptions<TradingOptions> tradingOptions, IStocksService stocksService, IFinnhubService finnhubService, IConfiguration configuration) : ViewComponent
    {
        private readonly TradingOptions _tradingOptions = tradingOptions.Value;
        private readonly IStocksService _stocksService = stocksService;
        private readonly IFinnhubService _finnhubService = finnhubService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileDict = null;

            if (stockSymbol != null)
            {
                companyProfileDict = await _finnhubService.GetCompanyProfile(stockSymbol);
                var stockPriceDict = await _finnhubService.GetStockPriceQuote(stockSymbol);
                if (stockPriceDict != null && companyProfileDict != null)
                {
                    companyProfileDict.Add("price", stockPriceDict["c"]);
                }
            }

            if (companyProfileDict != null && companyProfileDict.ContainsKey("logo"))
                return View(companyProfileDict);
            else
                return Content("");
        }
    }
}
