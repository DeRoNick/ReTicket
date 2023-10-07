namespace ReTicket.Domain.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal TicketPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}