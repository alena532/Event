using Events.DBContext;
using Events.DTO;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.Repository;

public class AdminRepository
{
    private readonly EventsContext _context;
    public AdminRepository(EventsContext context)
    {
        _context = context;

    }
    public async Task<IEnumerable<Event>> GetEventsWithCompanySpeaker()
    {
        return await _context.Events.Include(c => c.Company).Include(c => c.Speaker).ToListAsync();
    }
    
    public async Task<Event> GetEventWithCompanySpeaker(int id)
    {
        return await _context.Events.Where(x=>x.Id==id).Include(x=>x.Company).Include(x=>x.Speaker).FirstOrDefaultAsync();
    }
    
    public async Task<Company> GetCompany(int id)
    {
        return await _context.Companies.FindAsync(id);
    }
    
    public async Task<Speaker> GetSpeaker(int id)
    {
        return await _context.Speakers.FindAsync(id);
    }
    
    public async Task CreateEvent(Event ev)
    {
        await _context.Events.AddAsync(ev);
        _context.SaveChanges();
    }
    
    public void DeleteEvent(Event ev)
    {
        _context.Events.Remove(ev);
        _context.SaveChanges();
    }
    
    public async Task<Event> GetEvent(int id)
    {
        return await _context.Events.FindAsync(id);
    }
    
    
    
}