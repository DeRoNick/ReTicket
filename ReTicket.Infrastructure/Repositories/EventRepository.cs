using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContext _dbContext;
        private readonly BaseRepository<Event> _baseRepository;

        public EventRepository(DbContext dbContext, BaseRepository<Event> baseRepository)
        {
            _dbContext = dbContext;
            _baseRepository = baseRepository;
        }

        public async Task<int> CreateAsync(Event @event, CancellationToken cancellationToken)
        {
            await _baseRepository.BaseAddAsync(@event, cancellationToken);
            return @event.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var @event = await _baseRepository.BaseGetAsync(cancellationToken, id);
            await _baseRepository.BaseDeleteAsync(@event, cancellationToken);
        }

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _baseRepository.BaseGetAllAsync(cancellationToken);
        }

        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _baseRepository.BaseGetAsync(cancellationToken, id);
        }

        public async Task UpdateAsync(Event @event, CancellationToken cancellationToken)
        {
            await _baseRepository.BaseUpdateAsync(@event, cancellationToken);
        }
    }

}
