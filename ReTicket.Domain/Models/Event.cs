namespace ReTicket.Domain.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TicketPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public ICollection<TicketListing> TicketListings { get; set; } = new List<TicketListing>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}