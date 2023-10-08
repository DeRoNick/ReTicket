using ReTicket.Domain.Enums;

namespace ReTicket.MVC.Models
{
    public class TicketViewModel
    {
        public string EventName { get; set; }
        public string QRCode { get; set; }
        public TicketStatus Status { get; set; }
    }
}
