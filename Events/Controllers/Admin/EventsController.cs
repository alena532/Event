using System.Net;
using System.Web.Http;
using Events.DBContext;
using AutoMapper;
using Events.Common.Attributes;
using Events.Contracts.Requests.Events;
using Events.Contracts.Responses.Events;
using Events.Service.IService;
namespace Events.Controllers;
using Microsoft.AspNetCore.Mvc;
using Events.Models;
using Microsoft.AspNetCore.Identity;

[ApiController]
[Authorize(Roles="Admin")]
[Route("api/admin/[controller]")]
public class EventsController:ControllerBase
{
    private readonly IEventsService _eventsService;

    public EventsController( IEventsService eventsService)
    {
        _eventsService = eventsService;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetEventResponse>>> GetAllAsync()
    {
        return Ok(await _eventsService.GetAllAsync());
    }
    
   /* [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventRequest ev)
    {
        _adminService.CreateEvent(ev);
        return Ok();
    }
    */
    [HttpGet("Company")]
    public async Task<ActionResult<ICollection<GetEventResponse>>> GetAllByCompanyAsync(int companyId)
    {
        return Ok(await _eventsService.GetByCompanyAsync(companyId));
    }
    
    [HttpGet("Id")]
    public async Task<ActionResult<ICollection<GetEventResponse>>> GetByIdAsync(int companyId)
    {
        return Ok(await _eventsService.GetByIdAsync(companyId));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _eventsService.DeleteAsync(id);
       return Ok();
    }
    
    [HttpPost("Create")]
    [ValidationModel]
    public async Task<ActionResult<GetEventResponse>> CreateAsync(CreateEventRequest request)
        => Ok(await _eventsService.CreateAsync(request));
    
}