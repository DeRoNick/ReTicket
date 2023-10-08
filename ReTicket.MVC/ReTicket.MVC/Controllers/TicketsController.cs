﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QRCoder;
using ReTicket.Application.Events.Queries;
using ReTicket.Application.Tickets.Query;
using ReTicket.Domain.Models;
using ReTicket.MVC.Helper;
using ReTicket.MVC.Models;

namespace ReTicket.MVC.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int eventId)
        {
            return View(await _mediator.Send(new GetTicketsByEventId.Query(eventId)));
        }

        public async Task<IActionResult> IndexForUser(string userId)
        {
            var tickets = await _mediator.Send(new GetTicketsByUser.Query(userId));

            var ticketViewModelList = new List<TicketViewModel>();

            foreach (var ticket in tickets)
            {
                var eventModel = await _mediator.Send(new GetEvent.Query(ticket.EventId));

                var base64QrCode = QRBase64Helper.Generate(ticket.Code);

                var ticketViewModel = new TicketViewModel 
                { 
                    EventName = eventModel.Name, 
                    QRCode = base64QrCode,
                    Status = ticket.Status
                };

                ticketViewModelList.Add(ticketViewModel);
            }

            return View(ticketViewModelList);
        }

        //// GET: Tickets
        //public async Task<IActionResult> Index()
        //{
        //    var reTicketMVCContext = _context.Ticket.Include(t => t.Event).Include(t => t.User);
        //    return View(await reTicketMVCContext.ToListAsync());
        //}

        //// GET: Tickets/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticket = await _context.Ticket
        //        .Include(t => t.Event)
        //        .Include(t => t.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticket);
        //}

        //// GET: Tickets/Create
        //public IActionResult Create()
        //{
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location");
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
        //    return View();
        //}

        //// POST: Tickets/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,EventId,Code,Status,UserId")] Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(ticket);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location", ticket.EventId);
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", ticket.UserId);
        //    return View(ticket);
        //}

        //// GET: Tickets/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticket = await _context.Ticket.FindAsync(id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location", ticket.EventId);
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", ticket.UserId);
        //    return View(ticket);
        //}

        //// POST: Tickets/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,Code,Status,UserId")] Ticket ticket)
        //{
        //    if (id != ticket.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ticket);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TicketExists(ticket.Id))
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
        //    ViewData["EventId"] = new SelectList(_context.Set<Event>(), "Id", "Location", ticket.EventId);
        //    ViewData["UserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", ticket.UserId);
        //    return View(ticket);
        //}

        //// GET: Tickets/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticket = await _context.Ticket
        //        .Include(t => t.Event)
        //        .Include(t => t.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticket);
        //}

        //// POST: Tickets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Ticket == null)
        //    {
        //        return Problem("Entity set 'ReTicketMVCContext.Ticket'  is null.");
        //    }
        //    var ticket = await _context.Ticket.FindAsync(id);
        //    if (ticket != null)
        //    {
        //        _context.Ticket.Remove(ticket);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TicketExists(int id)
        //{
        //  return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
