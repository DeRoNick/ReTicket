using Microsoft.EntityFrameworkCore.Storage;
using ReTicket.Application.Abstractions;
using ReTicket.Persistence.Database;

namespace ReTicket.Persistence.Repositories
{
    public class TransactionProvider : ITransactionProvider
    {
        private readonly ReTicketDbContext _db;
        public TransactionProvider(ReTicketDbContext db)
        {
            _db = db;
        }

        public async Task<IDbContextTransaction> CreateTransactionScope(CancellationToken cancellationToken)
        {
            return await _db.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _ = await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
