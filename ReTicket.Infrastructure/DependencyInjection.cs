using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestEase.HttpClientFactory;
using ReTicket.Application.Infrastructure.Paypal;
using ReTicket.Infrastructure.RestEase;
using Stripe;

namespace ReTicket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRestEaseClient(configuration.GetSection("Paypal:Address").Value, new AddRestEaseClientOptions<IPaypalClient>()
        {
            InstanceConfigurer = instance =>
            {
                instance.Authorization = configuration.GetSection("Paypal:Authorization").Value;
            },
            RestClientConfigurer = client =>
            {
                client.JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };
            }
        });
        StripeConfiguration.ApiKey = configuration.GetSection("Stripe:SecretKey").Get<string>();
        return services; 
    }
}