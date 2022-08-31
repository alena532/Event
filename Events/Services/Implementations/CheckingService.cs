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

public class CheckingService:ICheckingService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    public CheckingService(UserManager<User> userManager,AppDbContext context,IMapper mapper)
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

    public async Task<Company> GetCompanyByIdAsync(int companyId)
    {
        var entity = await _context.Companies.FindAsync(companyId);
        if (entity != null) return entity;
        return null;
    }
    
    public async Task<Speaker> GetSpeakerByIdAsync(int speakerId)
    {
        var entity = await _context.Speakers.FindAsync(speakerId);
        if (entity != null) return entity;
        return null;
    }
    
    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        var entity = await _context.Events.FindAsync(eventId);
        if (entity != null) return entity;
        return null;
    }
    
}