using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReTicket.Domain.Models;

namespace ReTicket.MVC.Controllers
{
    public class TicketListingsController : Controller
    {

        //// GET: TicketListings
        //public async Task<IActionResult> Index()
        //{
        //    var reTicketMVCContext = _context.TicketListing.Include(t => t.Event).Include(t => t.Ticket).Include(t => t.User);
        //    return View(await reTicketMVCContext.ToListAsync());
        //}

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

        //// GET: TicketListings/Create
        //public IActionResult Create()
        //{
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location");
        //    ViewData["TicketId"] = new SelectList(_context.Set<Ticket>(), "Id", "UserId");
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
        //    return View();
        //}

        // POST: TicketListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketListing ticketListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ticketListing);
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
