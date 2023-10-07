namespace ReTicket.Application.Infrastructure.Paypal.CreateOrder;

public enum OrderStatus
{
    CREATED,
    SAVED,
    APPROVED,
    VOIDED,
    COMPLETED,
    PAYER_ACTION_REQUIRED
}

public class CreateOrderResponse
{
    public string Id { get; set; }
    public OrderStatus Status { get; set; }
}