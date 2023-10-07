using Microsoft.EntityFrameworkCore;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;
using System.Threading;

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

        public Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return BaseGetAsync(cancellationToken,id);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            await BaseUpdateAsync(ticket, cancellationToken);
        }
    }
}
