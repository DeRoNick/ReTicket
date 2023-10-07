namespace ReTicket.Domain.Models;

public class TicketListing
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    public decimal Price { get; set; }
    public int UserId { get; set; }
}