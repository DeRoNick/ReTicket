using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReTicket.Application.Infrastructure.Exceptions
{
    public class ApplicationValidationException : ApplicationException
    {
        public ApplicationValidationException()
            : base("Validation error occurred", "ValidationError", "One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ApplicationValidationException(string propertyName, string errorMessage)
            : this()
        {
            Failures.Add(propertyName, new string[] { errorMessage });
        }

        public ApplicationValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        [JsonExtensionData]
        public IDictionary<string, string[]> Failures { get; }
    }
}
