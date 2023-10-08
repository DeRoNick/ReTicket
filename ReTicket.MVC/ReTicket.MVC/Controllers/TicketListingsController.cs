using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.TicketListings.Query;
using ReTicket.Domain.Models;
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
            await _mediator.Send(new BuyListing.Command(ticketListingId, userId));

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
        //[HttpPost]

        //public async Task<IActionResult> Create(TicketListing ticketListing)
        //{
            //if (!_signInManager.IsSignedIn(User) || !ModelState.IsValid) return View();
            //var command = new CreateListing.Command(ticketListing.TicketId, ticketListing.Price,
            //    User.FindFirstValue(ClaimTypes.NameIdentifier));
            //return RedirectToAction("Details", await _mediator.Send(command));
        //}

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
