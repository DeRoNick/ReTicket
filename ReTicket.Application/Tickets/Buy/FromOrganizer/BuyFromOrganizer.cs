using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Enums;
using ApplicationException = ReTicket.Application.Infrastructure.Exceptions.ApplicationException;

namespace ReTicket.Application.Tickets.Buy.FromOrganizer;

public static class BuyFromOrganizer
{
    public class Command : IRequest
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _userRepo;
        private readonly IEventRepository _eventRepo;
        private readonly ITicketRepository _ticketRepo;
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1);
        
        public Handler(IUserRepository userRepo, IEventRepository eventRepo, ITicketRepository ticketRepo)
        {
            _userRepo = userRepo;
            _eventRepo = eventRepo;
            _ticketRepo = ticketRepo;
        }
        
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (user == null) throw new ApplicationException("No such user found");

            var @event = await _eventRepo.GetByIdAsync(request.EventId, cancellationToken).ConfigureAwait(false);
            if (@event == null) throw new ApplicationException("No such event exists");

            await _lock.WaitAsync(cancellationToken).ConfigureAwait(false);
            var ticket = await _ticketRepo.GetAllForEventAsync(request.EventId, cancellationToken).ConfigureAwait(false);
            var openTickets = ticket.Where(x => x.Status == TicketStatus.ForSale).ToList();
            if (openTickets.Count == 0)
            {
                _lock.Release();
                throw new ApplicationException("No more tickets left");
            }

            var ticketToBuy = openTickets[0];
            ticketToBuy.Status = TicketStatus.Sold;
            ticketToBuy.UserId = request.UserId;
            ticketToBuy.Code = Guid.NewGuid();

            await _ticketRepo.UpdateAsync(ticketToBuy, cancellationToken);
            _lock.Release();
        }
    }
}