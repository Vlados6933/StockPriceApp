using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StockPriceApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            IExceptionHandlerFeature? exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerFeature != null)
            {
                ViewBag.ErrorMessage = exceptionHandlerFeature.Error.Message;
            }

            return View();
        }
    }
}
