using AutoMapper;
using Events.Contracts.Requests.Auth;
using Events.Contracts.Responses.Auth;
using Events.Contracts.Responses.Users;
using Events.DBContext;
using Events.Models;
using Events.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Events.Service;

public class AuthService:IAuthService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    public AuthService(AppDbContext context,IMapper mapper,UserManager<User> userManager,IJwtService jwtService)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new BadHttpRequestException("Invalid username and (or) password");
        }

       // var roles = await _userManager.GetRolesAsync(user);

       User userWithRole = _context.Users.Where(x => x.Id == user.Id).Include(r => r.Role).FirstOrDefault();
        
        var token = _jwtService.GenerateJwtTokenAsync(userWithRole);
        var loginResponse = new LoginResponse
        {
            Token = token,
            User = _mapper.Map<GetAdminResponse>(userWithRole)
        };

        return loginResponse;
    }

}
