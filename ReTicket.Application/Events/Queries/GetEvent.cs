using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Events.Queries
{
    public static class GetEvent
    {
        public class Query : IRequest<Event?>
        {
            public int Id { get; }
            public Query(int id)
            {
                Id = id;
            }
        }
        internal sealed class Handler : IRequestHandler<Query, Event?>
        {
            private readonly IEventRepository _eventRepo;

            public Handler(IEventRepository eventRepo)
            {
                _eventRepo = eventRepo;
            }
            public async Task<Event?> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _eventRepo.GetByIdAsync(request.Id, cancellationToken);
            }
        }
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                _ = RuleFor(x => x.Id).NotEmpty();
            }
        }
    }
}
