using ReTicket.Application.TicketListings;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task DeleteAsync(string name, CancellationToken cancellationToken)
        {
            //var result = await GetAsync(name, cancellationToken);

            if (result != null)
            {
                await BaseDeleteAsync(result, cancellationToken);
            }
        }

    }
}
