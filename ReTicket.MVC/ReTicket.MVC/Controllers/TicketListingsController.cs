using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTicket.Application.Events.Queries;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.TicketListings.Query;
using ReTicket.Domain.Models;

namespace ReTicket.MVC.Controllers
{
    public class TicketListingsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<IdentityUser> _signInManager;
        public TicketListingsController(IMediator mediator, SignInManager<IdentityUser> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }

        // GET: TicketListings
        public async Task<IActionResult> Index(int eventId)
        {
            return View(await _mediator.Send(new GetTicketListingsByEventId.Query(eventId))); //TODO NOW
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int ticketListingId, string userId)
        {
            var listing = await _mediator.Send(new GetListingById.Query() { Id = ticketListingId});

            new CheckOutController().CheckOut(listing.Price, listing.EventName, listing.TicketCode);

            await _mediator.Send(new BuyListing.Command(ticketListingId, userId));

            return RedirectToAction("Index");
        }

        // GET: TicketListings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]

        public async Task<IActionResult> Create(TicketListing ticketListing)
        {
            if (!_signInManager.IsSignedIn(User) || !ModelState.IsValid) return View();
            var command = new CreateListing.Command(ticketListing.TicketId, ticketListing.Price,
                User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Details", await _mediator.Send(command));
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
