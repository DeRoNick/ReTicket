using Microsoft.EntityFrameworkCore;
using ReTicket.Application.Tickets;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;

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

        public async Task<List<Ticket>> GetAllForEvent(int eventId, CancellationToken cancellationToken)
        {
            var query = GetQuery().Where(x => x.EventId == eventId);
            return await query.ToListAsync(cancellationToken);
        }

        public Task<Ticket> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return GetByIdAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            await UpdateAsync(ticket, cancellationToken);
        }
    }
}
