using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;

namespace ReTicket.MVC.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckOut(decimal price, string eventName, string ticketCode) 
        {
            var domain = "https://localhost:7067";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + $"CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>()
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal= price,
                            Currency = "GEL",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = eventName + " " + ticketCode,
                            },

                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
