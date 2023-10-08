using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.TicketListings.Query;
using ReTicket.MVC.Models;
using Stripe.Checkout;
using Stripe;
using ReTicket.Persistence.Repositories;

namespace ReTicket.MVC.Controllers
{
    public class TicketListingsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        public TicketListingsController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET: TicketListings
        public async Task<IActionResult> Index(int eventId)
        {
            var ticketListings = await _mediator.Send(new GetTicketListingsByEventId.Query(eventId));

            var ticketListingsViewModel = new List<TicketListingViewModel>();

            foreach (var ticketListing in ticketListings)
            {
                var identityUser = await _userManager.FindByIdAsync(ticketListing.UserId);

                var ticketListingViewModel = new TicketListingViewModel
                {
                    SellerUsername = identityUser.UserName,
                    Price = ticketListing.Price,
                    TicketListingId = ticketListing.Id
                };
                ticketListingsViewModel.Add(ticketListingViewModel);
            }
            return View(ticketListingsViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Buy(int ticketListingId, string userId)
        {
            var listing = await _mediator.Send(new GetListingById.Query(ticketListingId));
            var domain = "https://localhost:7067/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"CheckOut/OrderConfirmation?ticketListingId={ticketListingId}",
                CancelUrl = domain + $"CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>()
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal= listing.Price*100,
                            Currency = "GEL",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = listing.Event.Name + " " + listing.Ticket.Code,
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

        // GET: TicketListings/Create
        public IActionResult Create()
        {
            return View();
        }

    }
}
