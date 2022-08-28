using System.Net;
using System.Web.Http;
using Events.DBContext;
using Events.DTO;
using AutoMapper;
using Events.Service.IService;
namespace Events.Controllers;
using Microsoft.AspNetCore.Mvc;
using Events.Models;
using Microsoft.AspNetCore.Identity;

[ApiController]
[Route("[controller]/[action]")]
public class AdminController:ControllerBase
{
    private readonly EventsContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IAdminService _adminService;

    public AdminController(EventsContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper,IAdminService adminService)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _adminService = adminService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<EventTransferDTO>> GetEvents()
    {
        return await _adminService.GetEvents();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventCreateDTO ev)
    {
        _adminService.CreateEvent(ev);
        return Ok();
    }
    
    [HttpGet]
    public async Task<EventTransferDTO> GetEvent(int id)
    {
        return await _adminService.GetEvent(id);
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        try
        { 
            await _adminService.DeleteEvent(id);
        }
        catch
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        return Ok();
    }
    
}