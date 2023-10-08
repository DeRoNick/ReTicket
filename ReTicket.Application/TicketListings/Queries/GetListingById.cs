using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Application.Infrastructure.Exceptions;
using ReTicket.Domain.Models;
using ApplicationException = ReTicket.Application.Infrastructure.Exceptions.ApplicationException;

namespace ReTicket.Application.TicketListings.Queries;

public static class GetListingById
{
    public class Query : IRequest<Response>
    {
        public int Id { get; set; }   
    }

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly ITicketListingRepository _repo;

        public Handler(ITicketListingRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (result == null) throw new ApplicationException("No Ticket Listing with given Id found");
            return new Response
            {
                Id = result.Id,
                UserId = result.UserId,
                TicketId = result.TicketId,
                EventId = result.EventId,
                Price = result.Price
            };
        }
    }

    public class Response
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
    }
    
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            _ = RuleFor(x => x.Id)
                .Must(x => x > 0).WithMessage("Id must be greater than 0.");
        }
    }
}