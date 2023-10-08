using Microsoft.EntityFrameworkCore.Storage;

namespace ReTicket.Application.Abstractions
{
    public interface ITransactionProvider
    {
        public Task<IDbContextTransaction> CreateTransactionScope(CancellationToken cancellationToken);
        public Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
