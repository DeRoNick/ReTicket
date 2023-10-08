using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ReTicket.Application.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.Infrastructure.MediatorPipes
{
    public class ValidationPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationPipe<TRequest, TResponse>> _logger;

        public ValidationPipe(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationPipe<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(x => x.Errors).Where(x => x != null).ToList();
            if (!failures.Any()) return await next();
            var requestType = request.GetType();
            var requestName = $"{(requestType.ReflectedType != null ? requestType.ReflectedType.FullName : requestType.FullName)}:{requestType.Name}";
            _logger.LogWarning("Validation errors: [{CommandType}] - Command: {@Command} - Errors: {@ValidationErrors}", requestName, request, failures);
            throw new ApplicationValidationException(failures);
        }
    }
}
