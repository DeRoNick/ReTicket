using Microsoft.EntityFrameworkCore;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;

namespace ReTicket.Persistence.Repositories
{
    public class TicketListingRepository : BaseRepository<TicketListing>, ITicketListingRepository
    {
        private readonly ReTicketDbContext _db;
        public TicketListingRepository(ReTicketDbContext context) : base(context)
        { 
            _db = context;
        }

        public async Task<List<TicketListing>> GetAllForEventAsync(int eventId, CancellationToken cancellationToken)
        {
            return await GetQuery().Where(x => x.EventId == eventId).ToListAsync(cancellationToken);
        }

        public async Task<TicketListing?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.TicketListings.Include(x => x.Ticket).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> InsertAsync(TicketListing ticket, CancellationToken cancellationToken)
        {
            await BaseAddAsync(ticket, cancellationToken);
            return ticket.Id;
        }

        public async Task UpdateAsync(TicketListing ticket, CancellationToken cancellationToken)
        {
            await BaseUpdateAsync(ticket, cancellationToken);
        }
    }
}
