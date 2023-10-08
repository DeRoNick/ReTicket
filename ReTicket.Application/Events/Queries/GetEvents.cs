using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Events.Queries
{
    public static class GetEvents
    {
        public class Query : IRequest<List<Event>>
        {
        }
        internal sealed class Handler : IRequestHandler<Query, List<Event>>
        {
            private readonly IEventRepository _eventRepo;

            public Handler(IEventRepository eventRepo)
            {
                _eventRepo = eventRepo;
            }
            public async Task<List<Event>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _eventRepo.GetAllAsync(cancellationToken);
            }
        }
        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
            }
        }
    }
}
