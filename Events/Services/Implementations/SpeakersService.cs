using Events.DBContext;
using Events.Models;
using Events.Service.IService;

namespace Events.Service;

public class SpeakersService:ISpeakersService
{
    private readonly AppDbContext _context;
    public SpeakersService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Speaker> GetByIdAsync(int id)
    {
        return await _context.Speakers.FindAsync(id);
        
    }
}