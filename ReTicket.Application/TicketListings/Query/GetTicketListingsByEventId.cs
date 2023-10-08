using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.TicketListings.Query
{
    public static class GetTicketListingsByEventId
    {
        public class Query : IRequest<List<TicketListing?>>
        {
            public int EventId { get; }
            public Query(int eventId)
            {
                EventId = eventId;
            }
        }
        public sealed class Handler : IRequestHandler<Query, List<TicketListing?>>
        {
            private readonly ITicketListingRepository _ticketListingRepo;

            public Handler(ITicketListingRepository ticketListingRepo)
            {
                _ticketListingRepo = ticketListingRepo;
            }
            public async Task<List<TicketListing?>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _ticketListingRepo.GetAllForEventAsync(request.EventId, cancellationToken);
            }
        }
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                _ = RuleFor(x => x.EventId)
                    .Must(x => x > 0).WithMessage("EventId must be greater than 0.");
            }
        }
    }
}
