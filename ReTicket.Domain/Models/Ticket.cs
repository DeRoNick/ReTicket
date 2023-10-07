using ReTicket.Domain.Enums;

namespace ReTicket.Domain.Models;

public class Ticket
{
    public int Id { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; }

    public ICollection<TicketListing> TicketListings { get; set; }

    public Guid Code { get; set; }

    public TicketStatus Status { get; set; }

    public string UserId { get; set; }
    public AppUser User { get; set; }
}