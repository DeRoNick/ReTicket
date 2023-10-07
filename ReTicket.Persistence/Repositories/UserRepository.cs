using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;

namespace ReTicket.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        public UserRepository(ReTicketDbContext context) : base(context)
        {

        }

        public async Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await BaseGetAsync(cancellationToken, id);
        }
    }
}
