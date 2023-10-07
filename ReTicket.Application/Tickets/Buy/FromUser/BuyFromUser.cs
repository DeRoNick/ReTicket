using MediatR;
using ReTicket.Application.Auth.Commands;
using ReTicket.Application.Infrastructure.Paypal;
using ReTicket.Application.Infrastructure.Paypal.CreateOrder;

namespace ReTicket.Application.Tickets.Buy.FromUser;

public static class BuyFromUser
{
    public class Command : IRequest<Response>
    {
        public int BuyerId { get; set; }
        public string SellerEmail { get; set; }
        public double Price { get; set; }
        public int TicketId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IPaypalClient _client;

        public Handler(IPaypalClient client)
        {
            _client = client;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            await _client.CreateOrder(new CreateOrderRequest
            {
                PurchaseUnits = new[]
                {
                    new PurchaseUnit
                    {
                        Amount = new Amount
                        {
                            Value = request.Price.ToString()
                        },
                        Payee = new Payee
                        {
                            EmailAddress = request.SellerEmail
                        },
                        Description = ""
                    }
                }
            });
            return new Response();
        }
    }

    public class Response
    {
        
    }
}