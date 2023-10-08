using ReTicket.Domain.Models;

namespace ReTicket.Application.Abstractions
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
        Task DeleteAsync(string title, CancellationToken cancellationToken);
        Task<List<Ticket>> GetAllForEventAsync(int eventId, CancellationToken cancellationToken);
        Task<List<Ticket>> GetAllForUserAsync(string userId, CancellationToken cancellationToken);
    }
}
