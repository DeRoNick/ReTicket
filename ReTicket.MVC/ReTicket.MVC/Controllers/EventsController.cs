using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReTicket.Application.Events.Queries;
using ReTicket.Domain.Models;

namespace ReTicket.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: Events
        public async Task<IActionResult> Index()
        {
            await _mediator.Send(new GetEvents.Command());
            return View();
        }

        //// GET: Events/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Event == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Event
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@event);
        //}

        //// GET: Events/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Events/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,TicketPrice,StartDate,EndDate,Location")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@event);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(@event);
        //}

        //// GET: Events/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Event == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Event.FindAsync(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(@event);
        //}

        //// POST: Events/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TicketPrice,StartDate,EndDate,Location")] Event @event)
        //{
        //    if (id != @event.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(@event);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EventExists(@event.Id))
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
        //    return View(@event);
        //}

        //// GET: Events/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Event == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Event
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@event);
        //}

        //// POST: Events/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Event == null)
        //    {
        //        return Problem("Entity set 'ReTicketMVCContext.Event'  is null.");
        //    }
        //    var @event = await _context.Event.FindAsync(id);
        //    if (@event != null)
        //    {
        //        _context.Event.Remove(@event);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool EventExists(int id)
        //{
        //  return (_context.Event?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
