using FluentValidation;
using MediatR;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;

namespace ReTicket.Application.Events.Queries
{
    public static class GetEvent
    {
        public class Command : IRequest<Event?>
        {
            public int Id { get; }
            public Command(int id)
            {
                Id = id;
            }
        }
        public sealed class Handler : IRequestHandler<Command, Event?>
        {
            private readonly IEventRepository _eventRepo;

            public Handler(IEventRepository eventRepo)
            {
                _eventRepo = eventRepo;
            }
            public async Task<Event?> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _eventRepo.GetByIdAsync(request.Id, cancellationToken);
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                _ = RuleFor(x => x.Id)
                    .Must(x => x > 0).WithMessage("Id must be a positive number");
            }
        }
    }
}
