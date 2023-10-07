using Microsoft.EntityFrameworkCore;
using ReTicket.Application.TicketListings;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;

namespace ReTicket.Infrastructure.Repositories
{
    public class TicketListingRepository : BaseRepository<TicketListing>, ITicketListingRepository
    {
        public TicketListingRepository(ReTicketDbContext context) : base(context) { }

        public async Task<int> CreateAsync(TicketListing author, CancellationToken cancellationToken)
        {
            await base.BaseAddAsync(author, cancellationToken);
            return author.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await GetByIdAsync(id, cancellationToken);

            if (result != null)
            {
                await BaseDeleteAsync(result, cancellationToken);
            }
        }

        public Task GenerateNewGuidAsync(TicketListing ticket, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TicketListing>> GetAllForEventAsync(int eventId, CancellationToken cancellationToken)
        {
            return await GetQuery().Where(x => x.EventId == eventId).ToListAsync(cancellationToken);
        }

        public Task<TicketListing?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return BaseGetAsync(cancellationToken, id);
        }

        public Task<int> InsertAsync(TicketListing ticket, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TicketListing ticket, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
