using AutoMapper;
using Events.Contracts.Requests.Users;
using Events.Contracts.Responses.Users;
using Events.DBContext;
using Events.Models;
using Events.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Events.Service;

public class CreateService:ICreateService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    public CreateService(UserManager<User> userManager,AppDbContext context,IMapper mapper)
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
}