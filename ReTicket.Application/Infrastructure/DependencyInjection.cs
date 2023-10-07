using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReTicket.Application.Infrastructure.MediatorPipes;

namespace ReTicket.Application.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));

        return services;
    }
}