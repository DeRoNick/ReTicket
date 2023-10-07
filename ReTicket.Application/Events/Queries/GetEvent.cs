using FluentValidation;
using MediatR;
using ReTicket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.Events.Queries
{
    public static class GetEvent
    {
        public class Command : IRequest<Event>
        {
            public string Title { get; }
            public Command(string title)
            {
                Title = title;
            }
        }
        internal sealed class Handler : IRequestHandler<Command, Event>
        {
            private readonly IEventRepository _eventRepo;

            public Handler(IEventRepository eventRepo)
            {
                _eventRepo = eventRepo;
            }
            public async Task<Event> Handle(Command request, CancellationToken cancellationToken)
            {
                //var result = (await _bookRepo.GetByIdAsync(id, cancellationToken)).Adapt<BookDetailsResponseModel>();
                //if (result == null)
                //    throw new ItemNotFoundException("Book was not found");
                //return result;

                return await _eventRepo.GetAsync(request.Title, cancellationToken);
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
            }
        }
    }
}
