using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Contracts.Requests.Users;
using Events.Contracts.Responses.Users;
using Events.Service.IService;

namespace Events.Controllers;

[ApiController]
[Authorize(Roles="Admin")]
[Route("api/admin/[controller]")]

public class UsersController:ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetCompanyWorkerResponse>>> GetCompanyWorkersAsync()
        => Ok(await _usersService.GetCompanyWorkersAsync());
    

    [HttpPost("")]
    public async Task<ActionResult<ICollection<GetCompanyWorkerResponse>>> CreateAsync([FromBody] CreateCompanyWorkerRequest request)
        => Ok(await _usersService.CreateCompanyWorkerAsync(request));
    
    
    
}