using FluentValidation;
using MediatR;

namespace ReTicket.Application.Auth.Commands
{
    public static class LoginUser
    {
        public class Command : IRequest<int>
        {
            public string Email { get; }
            public string Password { get; }
            public Command(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
        internal sealed class Handler : IRequestHandler<Command, int>
        {
            private readonly object _authService;
            public Handler(object authService)
            {
                _authService = authService;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                //

                return 1;
            }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().EmailAddress().WithMessage("Your email is not in valid format.");
                RuleFor(p => p.Password)
                    .NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            }
        }
    }
}
