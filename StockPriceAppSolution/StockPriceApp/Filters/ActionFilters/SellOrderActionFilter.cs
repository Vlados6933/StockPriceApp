using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StockPriceApp.Controllers;
using StockPriceApp.Models;

namespace StockPriceApp.Filters.ActionFilters
{
    public class SellOrderActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController && context.ActionArguments["sellOrderRequest"] is SellOrderRequest sellOrderRequest)
            {
                sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

                tradeController.ModelState.Clear();

                tradeController.TryValidateModel(sellOrderRequest);

                if (!tradeController.ModelState.IsValid)
                {
                    tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
                    context.Result = tradeController.View("Index", stockTrade);
                }
            }

            await next();
        }
    }
}
