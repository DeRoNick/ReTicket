using ReTicket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.TicketListings
{
    public interface ITicketListingRepository
    {
        Task<TicketListing> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<TicketListing>> GetAllForEventAsync(string eventTitle, CancellationToken cancellationToken);
        Task GenerateNewGuidAsync(TicketListing ticket, CancellationToken cancellationToken);
        Task UpdateAsync(TicketListing ticket, CancellationToken cancellationToken);
        Task DeleteAsync(string title, CancellationToken cancellationToken);
    }
}
