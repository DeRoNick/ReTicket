using ReTicket.Domain.Models;

namespace ReTicket.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}
