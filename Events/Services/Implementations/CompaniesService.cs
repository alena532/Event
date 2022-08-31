using Events.DBContext;
using Events.Models;
using Events.Service.IService;

namespace Events.Service;

public class CompaniesService:ICompaniesService
{
    private readonly AppDbContext _context;
    public CompaniesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Company> GetByIdAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }
    
   
}