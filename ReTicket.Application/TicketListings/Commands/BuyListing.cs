using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Rules;
using ReTicket.Domain.Models;

namespace ReTicket.Application.TicketListings.Commands
{
    public static class BuyListing
    {
        public class Command : IRequest
        {
            public Command(int listingId, string userId)
            {
                UserId = userId;
                ListingId = listingId;
            }
            internal int ListingId { get; }
            internal string UserId { get; }

        }
        internal sealed class Handler : IRequestHandler<Command>
        {
            private readonly ITicketListingRepository _listingRepository;
            private readonly IUserRepository _userRepository;
            private readonly ITicketRepository _ticketRepository;
            private readonly PriceRuleOptions _priceRuleOptions;
            private readonly ITransactionProvider _transactionProvider;

            public Handler(ITicketListingRepository listingRepo, ITicketRepository ticketRepository, IUserRepository userRepository, PriceRuleOptions options, ITransactionProvider transactionProvider)
            {
                _listingRepository = listingRepo;
                _ticketRepository = ticketRepository;
                _userRepository = userRepository;
                _priceRuleOptions = options;
                _transactionProvider = transactionProvider;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                    throw new Infrastructure.Exceptions.ApplicationException("User with that Id does not exist.");

                var listing = await _listingRepository.GetByIdAsync(request.ListingId, cancellationToken);
                if (listing == null)
                    throw new Infrastructure.Exceptions.ApplicationException("Ticket Listing with that Id does not exist.");

                var ticket = await _ticketRepository.GetByIdAsync(listing.TicketId, cancellationToken);
                if (ticket == null)
                    throw new Infrastructure.Exceptions.ApplicationException("Ticket with that Id does not exist.");

                try
                {
                    var commissionAmount = GetCommissionAmount(listing);
                    //external paying service, make them pay listing amount + commission amount, transfer commissionamount to companys account

                }
                catch (Exception e)
                {
                    throw new Infrastructure.Exceptions.ApplicationException("Error During payment process, try again later.", e.Message);
                }


                using (var dbContextTransaction = await _transactionProvider.CreateTransactionScope(cancellationToken))
                {
                    try
                    {
                        ticket.Code = Guid.NewGuid();
                        ticket.UserId = user.Id;
                        ticket.Status = Domain.Enums.TicketStatus.Sold;

                        await _ticketRepository.UpdateAsync(ticket, cancellationToken);

                        await _transactionProvider.SaveChangesAsync(cancellationToken);
                        await dbContextTransaction.CommitAsync(cancellationToken);

                    }
                    catch (Exception e)
                    {
                        //refund
                        await dbContextTransaction.RollbackAsync(cancellationToken);
                        throw new Infrastructure.Exceptions.ApplicationException(e.Message);
                    }
                }
            }

            private decimal GetCommissionAmount(TicketListing listing)
            {
                return listing.Price * _priceRuleOptions.MarginCommissionPercentage / 100;
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                _ = RuleFor(x => x.ListingId).GreaterThan(0);
                _ = RuleFor(x => x.UserId).NotEmpty();

            }
        }
    }
}
