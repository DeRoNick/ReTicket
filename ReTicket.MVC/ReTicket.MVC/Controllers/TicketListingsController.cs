using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReTicket.Application.TicketListings.Commands;
using ReTicket.Application.TicketListings.Queries;
using ReTicket.Application.Events.Queries;
using ReTicket.Application.TicketListings.Commands;
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
            return View(await _mediator.Send(new GetTicketListingsByEventId.Query(eventId)));
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string eventId, string userId)
        {
            //get ticketId by eventid

            int listingId = 5;//= //await _mediator.Send()

            await _mediator.Send(new BuyListing.Command(listingId, userId));

            return View();
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
            return RedirectToAction("GetListing", await _mediator.Send(command));
        }

        public async Task<IActionResult> Details(int listingId)
        {
            var command = new GetListingById.Query
            {
                Id = listingId
            };
            return View(await _mediator.Send(command));
        }

        //// GET: TicketListings/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.TicketListing == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticketListing = await _context.TicketListing.FindAsync(id);
        //    if (ticketListing == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location", ticketListing.EventId);
        //    ViewData["TicketId"] = new SelectList(_context.Set<Ticket>(), "Id", "UserId", ticketListing.TicketId);
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", ticketListing.UserId);
        //    return View(ticketListing);
        //}

        //// POST: TicketListings/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,EventId,UserId,Price")] TicketListing ticketListing)
        //{
        //    if (id != ticketListing.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ticketListing);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TicketListingExists(ticketListing.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location", ticketListing.EventId);
        //    ViewData["TicketId"] = new SelectList(_context.Set<Ticket>(), "Id", "UserId", ticketListing.TicketId);
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", ticketListing.UserId);
        //    return View(ticketListing);
        //}

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

        //// POST: TicketListings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.TicketListing == null)
        //    {
        //        return Problem("Entity set 'ReTicketMVCContext.TicketListing'  is null.");
        //    }
        //    var ticketListing = await _context.TicketListing.FindAsync(id);
        //    if (ticketListing != null)
        //    {
        //        _context.TicketListing.Remove(ticketListing);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TicketListingExists(int id)
        //{
        //  return (_context.TicketListing?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
