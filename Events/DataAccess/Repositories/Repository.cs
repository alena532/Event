using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Events.DBContext.Repositories.Base;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.DBContext.Repositories;

public class Repository : IRepository<Event> {
    
    protected readonly AppDbContext _context;
    
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Event> AddAsync(Event entity) {
        await _context.Set<Event>().AddAsync(entity);
        await SaveAsync();
        return entity;
    }
    
    
    public async Task DeleteAsync<Event>(Models.Event? entity)
    {
        var dbSet = _context.Set<Models.Event>();
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
        await SaveAsync();
    }
    
    public IQueryable<Event> GetQuearble(Expression<Func<Event, bool>> filter) 
    {
        IQueryable<Event> query = _context.Set<Event>().Include(c => c.Company).Include(c => c.Speaker);
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        return query;
    }
    
    public async Task<IReadOnlyList<Event>> GetAllAsync(Expression<Func<Event, bool>> filter = null)
    {
        return await GetQuearble(filter).ToListAsync();
        
    }
    
    public async Task<Event> GetByIdAsync(int id)
    {
        return await _context.Set<Event>().Where(x => x.Id == id).Include(s=>s.Speaker).Include(s=>s.Company).FirstOrDefaultAsync();
    }
    
    public async Task UpdateAsync(Event entity)
    {
        if (entity == null) throw new ArgumentNullException();
        
        _context.Set<Event>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        SaveAsync();
    }
    
    public Task SaveAsync()
    {
        try
        {
            return  _context.SaveChangesAsync();
        }
        catch (ValidationException e)
        {
            throw new ValidationException(e.Message, e.InnerException);
        }
    }
}