using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.TicketListings.Query;
using ReTicket.MVC.Models;

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
            var listing = await _mediator.Send(new GetListingById.Query() { Id = ticketListingId });
            return RedirectToAction("CheckOut", controllerName: "CheckOut", new { price = listing.Price, eventName = listing.EventName, ticketCode = listing.TicketCode });
            await _mediator.Send(new BuyListing.Command(ticketListingId, userId));
            return View();
        }

        // GET: TicketListings/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int listingId)
        {
            var command = new GetListingById.Query
            {
                Id = listingId
            };
            return View(await _mediator.Send(command));
        }

    }
}
