using System.Linq.Expressions;
using UP.Domain;

namespace UP.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAllAsync(string? includeproperties = null, Expression<Func<T, bool>>? filter = null);
    Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, string? includeproperties = null);
    Task<T> CreateAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);
    Task<int> BulkDeleteAsync(IEnumerable<T> entities);
    Task<int> BulkAdd(IEnumerable<T> entities);
}