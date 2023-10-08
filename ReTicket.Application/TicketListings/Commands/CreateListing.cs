using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Rules;
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

            public int TicketId { get; }
            public decimal Price { get; }
            public string UserId { get; }

        }
        public sealed class Handler : IRequestHandler<Command, int>
        {
            private readonly ITicketListingRepository _listingRepository;
            private readonly IUserRepository _userRepository;
            private readonly ITicketRepository _ticketRepository;
            private readonly PriceRuleOptions _priceRuleOptions;

            public Handler(ITicketListingRepository listingRepo, ITicketRepository ticketRepository, IUserRepository userRepository, PriceRuleOptions options)
            {
                _listingRepository = listingRepo;
                _ticketRepository = ticketRepository;
                _userRepository = userRepository;
                _priceRuleOptions = options;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                    throw new Infrastructure.Exceptions.ApplicationException("User with that Id does not exist.");

                var ticket = await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken);
                if (ticket == null)
                    throw new Infrastructure.Exceptions.ApplicationException("Ticket with that Id does not exist.");

                if (IsIllegalMargin(request, ticket))
                    throw new Infrastructure.Exceptions.ApplicationException("Price margin is higher than the legal limit");


                ticket.Status = Domain.Enums.TicketStatus.ForResale;

                var listing = new TicketListing() { Price = request.Price, TicketId = request.TicketId, UserId = request.UserId };

                await _ticketRepository.UpdateAsync(ticket, cancellationToken);

                return await _listingRepository.InsertAsync(listing, cancellationToken);
            }

            private bool IsIllegalMargin(Command request, Ticket ticket)
            {
                return (request.Price - ticket.Event.TicketPrice) / ticket.Event.TicketPrice * 100 > _priceRuleOptions.MaximumMarginPercentage;
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                _ = RuleFor(x => x.TicketId).GreaterThan(0);
                _ = RuleFor(x => x.Price).GreaterThan(0);
                _ = RuleFor(x => x.UserId).NotEmpty();

            }
        }
    }
}
