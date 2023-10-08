using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.TicketListings.Query;

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
            return View(await _mediator.Send(new GetTicketListingsByEventId.Query(eventId)));
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string eventId, string userId)
        {
            //get ticketId by eventid

            int listingId = 5;//= //await _mediator.Send()

            await _mediator.Send(new BuyListing.Command(listingId, userId));

            return RedirectToAction("Index");
        }


        //// GET: TicketListings/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.TicketListing == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticketListing = await _context.TicketListing
        //        .Include(t => t.Event)
        //        .Include(t => t.Ticket)
        //        .Include(t => t.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ticketListing == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticketListing);
        //}

        // GET: TicketListings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]

        public async Task<IActionResult> Create(CreateListing.Command ticketListing)
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

       

        //// GET: TicketListings/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.TicketListing == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticketListing = await _context.TicketListing
        //        .Include(t => t.Event)
        //        .Include(t => t.Ticket)
        //        .Include(t => t.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ticketListing == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticketListing);
        //}

    }
}
