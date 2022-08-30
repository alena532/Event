using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Events.ConfigurationOptions;
using Events.DBContext;
using Events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Events.Common.Attributes;
using Events.Contracts.Requests.Auth;
using Events.Contracts.Responses.Auth;
using Events.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly IAuthService _authService;
    public AuthController(AppDbContext context, UserManager<User> userManager, IOptions<JwtOptions> jwtOptions,IAuthService authService)
    {
        _context=context;
        _userManager = userManager;
        _jwtOptions = jwtOptions?.Value ?? throw new ArgumentNullException(nameof(JwtOptions));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        return Ok(result);
    }
    

}
