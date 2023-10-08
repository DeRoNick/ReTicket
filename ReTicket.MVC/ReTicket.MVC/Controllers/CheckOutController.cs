using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.Tickets.Query;
using ReTicket.MVC.Helper;
using Stripe.Checkout;
using System.Security.Claims;

namespace ReTicket.MVC.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;


        public CheckOutController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult CheckOut(decimal price, string eventName, string ticketCode) 
        //{
        //    var domain = "https://localhost:7067/";
        //    var options = new SessionCreateOptions
        //    {
        //        SuccessUrl = domain + $"CheckOut/OrderConfirmation?{}",
        //        CancelUrl = domain + $"CheckOut/Login",
        //        LineItems = new List<SessionLineItemOptions>()
        //        {
        //            new SessionLineItemOptions
        //            {
        //                PriceData = new SessionLineItemPriceDataOptions
        //                {
        //                    UnitAmountDecimal= price*100,
        //                    Currency = "GEL",
        //                    ProductData = new SessionLineItemPriceDataProductDataOptions
        //                    {
        //                        Name = eventName + " " + ticketCode,
        //                    },

        //                },
        //                Quantity = 1
        //            }
        //        },
        //        Mode = "payment",
        //    };
        //    var service = new SessionService();
        //    Session session = service.Create(options);
        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);
        //}

        public async Task<IActionResult> OrderConfirmation(int ticketListingId)
        {
            var user = await _userManager.GetUserAsync(User);

            await _mediator.Send(new BuyListing.Command(ticketListingId, user.Id));

            var ticket = await _mediator.Send(new GetTicketByListingId.Query(ticketListingId));

            var base64QRCodeString = QRBase64Helper.Generate(key: ticket.Code, qrCodeSize: 10);

            return View((object)base64QRCodeString);
        }
    }
}
