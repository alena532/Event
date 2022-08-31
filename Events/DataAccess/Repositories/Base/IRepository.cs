using System.Linq.Expressions;
using Events.Models;

namespace Events.DBContext.Repositories.Base;

public interface IRepository<T> where T:class
{
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
    Task<T> GetByIdAsync(int id);
    Task DeleteAsync(T entity);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task SaveAsync();

}