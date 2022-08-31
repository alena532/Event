using System.Linq.Expressions;
using Events.Models;

namespace Events.DBContext.Repositories.Base;

public interface IRepository<T> where T:class
{
    Task<IReadOnlyList<Event>> GetAllAsync(Expression<Func<Event, bool>> filter = null);
    Task<T> GetByIdAsync(int id);
    Task DeleteAsync<Event>(Models.Event? entity);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task SaveAsync();

}