namespace ReTicket.Application.Infrastructure.Paypal.CreateOrder;

public class Amount
{
    public string CurrencyNode { get; set; }
    public string Value { get; set; }
}

public class Item
{
    public string Name { get; set; }
    public string Quantity { get; set; }
    public string Description { get; set; }
    public string Category { get; private set; } = "DIGITAL_GOODS";
    public Amount UnitAmount { get; set; }
}

public class PurchaseUnit
{
    public string Description { get; set; }
    public Item[] Items { get; set; }
    public Amount Amount { get; set; }
    public Payee Payee { get; set; }
}

public class Payee
{
    public string EmailAddress { get; set; }
}

public class CreateOrderRequest
{
    public string Intent { get; private set; } = "CAPTURE";

    public PurchaseUnit[] PurchaseUnits { get; set; }
}