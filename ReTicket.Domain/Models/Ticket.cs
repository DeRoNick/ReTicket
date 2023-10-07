using ReTicket.Domain.Enums;

namespace ReTicket.Domain.Models;

public class Ticket
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }
    public Guid Guid { get; set; }
    public TicketStatus Status { get; set; }
}