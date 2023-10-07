using ReTicket.Domain.Models;

namespace ReTicket.Application.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Event>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Event @event, CancellationToken cancellationToken);
        Task<int> CreateAsync(Event @event, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
