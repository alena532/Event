using System.Linq;
using System.Linq.Expressions;
using Events.DBContext.Repositories.Base;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.DBContext.Repositories;

public class Repository : IRepository<Models.Event> {
    protected readonly AppDbContext _context;
    
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Models.Event> AddAsync(Models.Event entity) {
        await _context.Set<Models.Event>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task DeleteAsync(Event entity) {
        _context.Set<Event>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<Event>> GetAllAsync(Expression<Func<Event, bool>> filter = null) {
        if (filter != null)
        {
            IQueryable<Event> query = _context.Set<Event>();
            query = query.Where(filter);
            return await query.Include(c=>c.Company).Include(c=>c.Speaker).ToListAsync();
        }
        return await _context.Set<Event>().Include(c=>c.Company).Include(c=>c.Speaker).ToListAsync();
    }
    public async Task<Event> GetByIdAsync(int id) {
        return _context.Set<Event>().Where(x=>x.Id == id).Include(c=>c.Company).Include(c=>c.Speaker).FirstOrDefault();
    }
    public void UpdateAsync(Event entity)
    {
       // _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        //_context.SaveChanges();
        
    }
}