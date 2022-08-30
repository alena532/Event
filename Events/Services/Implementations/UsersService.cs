using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using Events.Contracts.Requests.Users;
using Events.Contracts.Responses.Users;
using Events.DBContext;
using Events.Models;
using Events.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Events.Service;

public class UsersService:IUsersService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    public UsersService(UserManager<User> userManager,AppDbContext context,IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetAdminResponse> CreateAdminAsync(CreateAdminRequest request)
    {
        Role userRole = _context.Roles.Where(x => x.Name == request.Role).FirstOrDefault();
        if(userRole == null) throw new ApplicationException("Role Not Found");
        
        User user = new()
        {
            FirstName = request.FirstName, LastName = request.LastName,
            UserName = request.UserName,
            Role = userRole,
        };
        if (await _userManager.FindByNameAsync(user.UserName) != null) return null;
        var result = await _userManager.CreateAsync(user, request.Password);
        if(!result.Succeeded) throw new ApplicationException(string.Join("\n", result.Errors));
        
        
        return _mapper.Map<GetAdminResponse>(user);

    }
    
    public async Task<GetCompanyWorkerResponse> CreateCompanyWorkerAsync(CreateCompanyWorkerRequest request)
    {
        Role userRole = _context.Roles.Where(x => x.Name == "CompanyWorker").FirstOrDefault();

        Company company = await _context.Companies.FindAsync(request.CompanyId);
        if(company == null) throw new ApplicationException("Company Not Found");
        User user = new()
        {
            FirstName = request.FirstName, LastName = request.LastName,
            UserName = request.UserName,
            Role = userRole,
            Company = company
        };
        if (await _userManager.FindByNameAsync(user.UserName) != null)
        {
            throw  new ApplicationException("username is already used");
        }
        var result = await _userManager.CreateAsync(user, request.Password);
        if(!result.Succeeded) throw new ApplicationException(string.Join("\n", result.Errors));
        
        
        return _mapper.Map<GetCompanyWorkerResponse>(user);

    }

    public async Task<ICollection<GetCompanyWorkerResponse>> GetCompanyWorkersAsync()
    {
        var entities = await _userManager.Users
            .Where(x => x.CompanyId != null)
            .Include(r=>r.Role)
            .Include(c=>c.Company)
            .ToListAsync();
        
        return _mapper.Map<ICollection<GetCompanyWorkerResponse>>(entities);
    }

    public async Task<Company> GetCompanyByCompanyIdAsync(int companyId)
    {
        var entity = await _context.Companies.FindAsync(companyId);
        if (entity != null) return entity;
        return null;
    }
    
    public async Task<Speaker> GetSpeakerBySpeakerIdAsync(int speakerId)
    {
        var entity = await _context.Speakers.FindAsync(speakerId);
        if (entity != null) return entity;
        return null;
    }
    
}