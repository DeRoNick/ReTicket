using Microsoft.EntityFrameworkCore;
using ReTicket.Application.Abstractions;
using ReTicket.Domain.Models;
using ReTicket.Persistence.Database;
using System.Linq.Expressions;

namespace ReTicket.Persistence.Repositories
{
    //public class UserRepository : IRepository<AppUser>
    //{
    //    private readonly ReTicketDbContext _context;
    //    public UserRepository(ReTicketDbContext dbContext)
    //    {
    //        _context = dbContext;
    //    }
    //    public async Task Delete(AppUser model, CancellationToken token)
    //    {
    //        _ = await Task.FromResult(_context.Remove(model));
    //    }

    //    public async Task<AppUser?> Get(Expression<Func<AppUser, bool>> predicate, CancellationToken token)
    //    {
    //        return await _context.Users.AsNoTracking().Where(predicate).FirstOrDefaultAsync(token);
    //    }

    //    public async Task<AppUser?> GetForUpdate(Expression<Func<AppUser, bool>> predicate, CancellationToken token)
    //    {
    //        return await _context.Users.Where(predicate).FirstOrDefaultAsync(token);
    //    }

    //    public async Task<ICollection<AppUser>> GetList(Expression<Func<AppUser, bool>> predicate, CancellationToken token)
    //    {
    //        return await _context.Users.AsNoTracking().Where(predicate).ToListAsync(token);
    //    }

    //    public async Task<ICollection<AppUser>> GetListForUpdate(Expression<Func<AppUser, bool>> predicate, CancellationToken token)
    //    {
    //        return await _context.Users.Where(predicate).ToListAsync(token);
    //    }

    //    public async Task<object> Insert(AppUser model, CancellationToken token)
    //    {
    //        _ = await _context.Users.AddAsync(model, token);
    //        return model.Id;
    //    }

    //    public async Task SaveChangesAsync(CancellationToken token)
    //    {
    //        _ = await _context.SaveChangesAsync(token);
    //    }

    //    public Task Update(AppUser model, CancellationToken token)
    //    {
    //        return Task.FromResult(_context.Users.Update(model));

    //    }
    //}
}
