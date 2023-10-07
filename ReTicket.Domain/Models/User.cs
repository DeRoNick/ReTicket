using Microsoft.AspNetCore.Identity;

namespace ReTicket.Domain.Models;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<TicketListing> ListedTickets { get; set; } = new LinkedList<TicketListing>();

}