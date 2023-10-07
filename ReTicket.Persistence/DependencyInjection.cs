using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReTicket.Application.Abstractions;
using ReTicket.Persistence.Database;
using ReTicket.Persistence.Repositories;

namespace ReTicket.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDbContext<ReTicketDbContext>(options =>
        {
            _ = options.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        _ = services.AddScoped<ITicketRepository, TicketRepository>();
        _ = services.AddScoped<ITicketListingRepository, TicketListingRepository>();
        _ = services.AddScoped<IEventRepository, EventRepository>();
        _ = services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}