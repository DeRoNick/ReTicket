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
            return View(await _mediator.Send(new GetEvents.Query()));
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var query = new GetEvent.Query((int)id);
            var @event = await _mediator.Send(query);
            if (@event == null)
                return NotFound();
            return View(@event);
        }
        
    }
}
