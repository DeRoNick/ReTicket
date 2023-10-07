using System.Linq.Expressions;

namespace ReTicket.Application.Abstractions;

public interface IRepository<T> where T : class
{
    Task<ICollection<T>> GetList(Expression<Func<T, bool>> predicate, CancellationToken token);
    Task<ICollection<T>> GetListForUpdate(Expression<Func<T, bool>> predicate, CancellationToken token);
    Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken token);
    Task<T> GetForUpdate(Expression<Func<T, bool>> predicate, CancellationToken token);
    Task Update(T model, CancellationToken token);
    Task Delete(T model, CancellationToken token);
    Task<int> Insert(T model, CancellationToken token);
}