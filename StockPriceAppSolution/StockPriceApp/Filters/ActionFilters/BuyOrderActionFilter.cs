using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StockPriceApp.Controllers;
using StockPriceApp.Models;

namespace StockPriceApp.Filters.ActionFilters
{
    public class BuyOrderActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController && context.ActionArguments["buyOrderRequest"] is BuyOrderRequest buyOrderRequest)
            {
                buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

                tradeController.ModelState.Clear();

                tradeController.TryValidateModel(buyOrderRequest);

                if (!tradeController.ModelState.IsValid)
                {
                    tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity, StockSymbol = buyOrderRequest.StockSymbol };
                    context.Result = tradeController.View("Index", stockTrade);
                }
            }

            await next();
        }
    }
}
