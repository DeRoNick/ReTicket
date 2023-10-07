using RestEase;
using ReTicket.Application.Infrastructure.Paypal.CreateOrder;

namespace ReTicket.Application.Infrastructure.Paypal;

[Header("Content-Type", "application/json")]
public interface IPaypalClient
{
    [Header("Authorization")] 
    public string Authorization { get; set; }
    
    [Post("v2/checkout/orders")]
    Task<CreateOrderResponse> CreateOrder([Body] CreateOrderRequest orderRequest);
}