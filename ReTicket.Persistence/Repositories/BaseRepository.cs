using Microsoft.EntityFrameworkCore;
using ReTicket.Persistence.Database;
using System.Linq.Expressions;

namespace ReTicket.Persistence.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected ReTicketDbContext _context;
        protected DbSet<T> _dbSet;
        protected IQueryable<T> Table => _dbSet;
        protected IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
        protected BaseRepository(ReTicketDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IQueryable<T> GetQuery()
        {
            return _dbSet;
        }
        public async Task BaseAddAsync(T entity, CancellationToken token)
        {
            await _dbSet.AddAsync(entity, token);
            _ = await _context.SaveChangesAsync(token);
        }
        public async Task<T?> BaseGetAsync(CancellationToken token, params object[] key)
        {
            return await _dbSet.FindAsync(key, token);
        }
        public async Task<List<T>> BaseGetAllAsync(CancellationToken token)
        {
            return await _dbSet.ToListAsync(token);
        }
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken token)
        {
            return _dbSet.AnyAsync(predicate, token);
        }
        public async Task BaseUpdateAsync(T entity, CancellationToken token)
        {
            if (entity == null)
                return;
            _dbSet.Update(entity);
            _ = await _context.SaveChangesAsync(token);
        }
        public async Task BaseDeleteAsync(T result, CancellationToken token)
        {
            _ = _dbSet.Remove(result);

            _ = await _context.SaveChangesAsync(token);
        }
    }
}