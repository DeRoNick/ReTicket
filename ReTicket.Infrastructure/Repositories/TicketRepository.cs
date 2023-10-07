using ReTicket.Application.Tickets;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ReTicketDbContext context) : base(context)
        {
        }

        public Task DeleteAsync(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllForEventAsync(string eventTitle, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
