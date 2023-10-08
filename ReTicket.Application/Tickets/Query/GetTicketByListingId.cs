using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.Tickets.Query
{
    public static class GetTicketByListingId
    {
        public class Query : IRequest<Ticket>
        {
            public int ListingId { get; }
            public Query(int listingId)
            {
                ListingId = listingId;
            }
        }
        public sealed class Handler : IRequestHandler<Query, Ticket>
        {
            private readonly ITicketListingRepository _ticketListingRepo;

            public Handler(ITicketListingRepository ticketListingRepo)
            {
                _ticketListingRepo = ticketListingRepo;
            }
            public async Task<Ticket> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _ticketListingRepo.GetByIdAsync(request.ListingId, cancellationToken)).Ticket;
            }
        }
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                _ = RuleFor(x => x.ListingId)
                    .GreaterThan(0);
            }
        }
    }
}
