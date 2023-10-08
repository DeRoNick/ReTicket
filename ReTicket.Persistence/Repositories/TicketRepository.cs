using Microsoft.EntityFrameworkCore;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;

namespace ReTicket.Persistence.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        private readonly ReTicketDbContext _db;
        public TicketRepository(ReTicketDbContext context) : base(context)
        {
            _db = context;
        }

        public Task DeleteAsync(string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> GetAllForEventAsync(int eventId, CancellationToken cancellationToken)
        {
            var query = GetQuery().Where(x => x.EventId == eventId);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<Ticket>> GetAllForUserAsync(string userId, CancellationToken cancellationToken)
        {
            var query = GetQuery().Where(x => x.UserId == userId);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.Tickets.Include(x => x.Event).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
        {
            await BaseUpdateAsync(ticket, cancellationToken);
        }
    }
}
