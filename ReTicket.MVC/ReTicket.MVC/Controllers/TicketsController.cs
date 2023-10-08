using System;
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

                var base64QrCode = QRBase64Helper.Generate(key: ticket.Code, qrCodeSize: 2);

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

    }
}
