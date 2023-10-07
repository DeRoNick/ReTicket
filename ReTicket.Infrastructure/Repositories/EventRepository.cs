using ReTicket.Application.Events;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ReTicketDbContext context) : base(context) { }

        public async Task<int> CreateAsync(Event @event, CancellationToken cancellationToken)
        {
            await base.BaseAddAsync(@event, cancellationToken);
            return @event.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var @event = await base.BaseGetAsync(cancellationToken, id);
            await base.BaseDeleteAsync(@event, cancellationToken);
        }

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await base.BaseGetAllAsync(cancellationToken);
        }

        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await base.BaseGetAsync(cancellationToken, id);
        }

        public async Task UpdateAsync(Event @event, CancellationToken cancellationToken)
        {
            await base.BaseUpdateAsync(@event, cancellationToken);
        }
    }

}
