using ReTicket.Domain.Models;

namespace ReTicket.Application.Users
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}
