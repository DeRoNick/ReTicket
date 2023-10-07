using ReTicket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Application.Tickets
{
    public interface ITicketRepository
    {
        Task<Ticket> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Ticket>> GetAllForEventAsync(string eventTitle, CancellationToken cancellationToken);
        Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
        //Task<int> BATCH CREATE? CreateAsync(Book book, CancellationToken cancellationToken);
        Task DeleteAsync(string title, CancellationToken cancellationToken);
    }
}
