using AutoMapper;
using Events.Contracts.Responses.Companies;
using Events.DBContext;
using Events.Models;
using Events.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Events.Service;

public class CompaniesService:ICompaniesService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public CompaniesService(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Company> GetByIdAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }
    
    public async Task<ICollection<GetCompanyResponse>> GetAllAsync()
    {
        var entities = await _context.Companies.ToListAsync();

        return _mapper.Map<ICollection<GetCompanyResponse>>(entities);
    }
   
}