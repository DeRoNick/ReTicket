using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;

namespace ReTicket.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {

        public EventRepository(ReTicketDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateAsync(Event @event, CancellationToken cancellationToken)
        {
            await BaseAddAsync(@event, cancellationToken);
            return @event.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var @event = await BaseGetAsync(cancellationToken, id);
            await BaseDeleteAsync(@event!, cancellationToken);
        }

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await BaseGetAllAsync(cancellationToken);
        }

        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await BaseGetAsync(cancellationToken, id);
        }

        public async Task UpdateAsync(Event @event, CancellationToken cancellationToken)
        {
            await BaseUpdateAsync(@event, cancellationToken);
        }
    }

}
