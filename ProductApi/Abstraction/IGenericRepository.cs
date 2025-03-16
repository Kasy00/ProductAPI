using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector);
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}