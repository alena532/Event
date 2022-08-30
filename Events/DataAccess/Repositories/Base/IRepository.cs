using System.Linq.Expressions;

namespace Events.DBContext.Repositories.Base;

public interface IRepository<T> where T:class
{
    Task <IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
    Task <T> GetByIdAsync(int id);
    Task <T> AddAsync(T entity); 
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    
}