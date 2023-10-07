using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;

namespace ReTicket.Application.TicketListings.Commands
{
    public static class CreateListing
    {
        public class Command : IRequest<int>
        {
            public Command(int ticketId, decimal price, string userId)
            {
                TicketId = ticketId;
                Price = price;
                UserId = userId;
            }

            internal int TicketId { get;}
            internal decimal Price { get;}
            internal string UserId { get;}
            
        }
        internal sealed class Handler : IRequestHandler<Command,int>
        {
            private readonly ITicketListingRepository _listingRepository;
            private readonly IUserRepository _userRepository;
            private readonly ITicketRepository _ticketRepository;

            public Handler(ITicketListingRepository listingRepo, ITicketRepository ticketRepository, IUserRepository userRepository)
            {
                _listingRepository = listingRepo;
                _ticketRepository = ticketRepository;
                _userRepository = userRepository;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null) 
                {
                    throw new Exceptions.ApplicationException("User with that Id does not exist.");
                }

                var ticket = await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken);
                if (ticket == null)
                {
                    throw new Exceptions.ApplicationException("Ticket with that Id does not exist.");
                }

                var listing = new TicketListing() { Price = request.Price, TicketId = request.TicketId, UserId = request.UserId};

                return await _listingRepository.InsertAsync(listing, cancellationToken);
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                _ = RuleFor(x => x.TicketId).GreaterThan(0);
                _ = RuleFor(x => x.UserId).NotEmpty();

            }
        }
    }
}
