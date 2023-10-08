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
    public static class GetTicketsByUser
    {
        public class Query : IRequest<List<Ticket?>>
        {
            public string UserId { get; }
            public Query(string userId)
            {
                UserId = userId;
            }
        }
        internal sealed class Handler : IRequestHandler<Query, List<Ticket?>>
        {
            private readonly ITicketRepository _ticketRepo;

            public Handler(ITicketRepository ticketRepo)
            {
                _ticketRepo = ticketRepo;
            }
            public async Task<List<Ticket?>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _ticketRepo.GetAllForUserAsync(request.UserId, cancellationToken);
            }
        }
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                _ = RuleFor(x => x.UserId).NotEmpty();
            }
        }
    }
}
