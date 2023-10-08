using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;

namespace ReTicket.Application.TicketListings.Queries;

public static class GetListingById
{
    public class Query : IRequest<TicketListing?>
    {
        public int Id { get; }
        public Query(int id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Query, TicketListing?>
    {
        private readonly ITicketListingRepository _repo;

        public Handler(ITicketListingRepository repo)
        {
            _repo = repo;
        }

        public async Task<TicketListing?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id, cancellationToken);
        }
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