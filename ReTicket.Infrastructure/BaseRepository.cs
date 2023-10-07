using ReTicket.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReTicket.Infrastructure
{
    public abstract class BaseRepository<T> where T : class
    {
        #region Protected
        protected ReTicketDbContext _context;
        protected DbSet<T> _dbSet;
        protected IQueryable<T> Table => _dbSet;
        protected IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
        #endregion

        #region ctor
        public BaseRepository(ReTicketDbContext context)
        {
            _context = context;
            //_dbSet = //TODO context.Set<T>();
        }
        #endregion

        #region Methods
        public async Task BaseAddAsync(T entity, CancellationToken token)
        {
            //TODO await _dbSet.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }
        public async Task<T> BaseGetAsync(CancellationToken token, params object[] key)
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
            //TODO _dbSet.Update(entity);
            await _context.SaveChangesAsync(token);
        }
        public async Task BaseDeleteAsync(T result, CancellationToken token)
        {
            _dbSet.Remove(result);

            await _context.SaveChangesAsync(token);
        }
        #endregion
    }
}
