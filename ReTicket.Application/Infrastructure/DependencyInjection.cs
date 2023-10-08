using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReTicket.Application.Infrastructure.MediatorPipes;
using ReTicket.Application.Rules;

namespace ReTicket.Application.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));

        var priceRuleOptions = new PriceRuleOptions();
        configuration.GetSection(PriceRuleOptions.PriceRule).Bind(priceRuleOptions);

        services.AddSingleton(priceRuleOptions);

        return services;
    }
}