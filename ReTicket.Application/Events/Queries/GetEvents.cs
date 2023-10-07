using FluentValidation;
using MediatR;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Events.Queries
{
    public static class GetEvents
    {
        public class Command : IRequest<List<Event>>
        {
        }
        internal sealed class Handler : IRequestHandler<Command, List<Event>>
        {
            private readonly IEventRepository _eventRepo;

            public Handler(IEventRepository eventRepo)
            {
                _eventRepo = eventRepo;
            }
            public async Task<List<Event>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _eventRepo.GetAllAsync(cancellationToken);
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
            }
        }
    }
}
