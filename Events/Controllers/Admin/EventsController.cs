using System.Web.Http;
using Events.Common.Attributes;
using Events.Contracts.Requests.Events;
using Events.Contracts.Responses.Events;
using Events.Service.IService;
namespace Events.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles="Admin")]
[Route("api/admin/[controller]")]
public class EventsController:ControllerBase
{
    private readonly IEventsService _eventsService;

    public EventsController(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<GetEventResponse>>> GetAllAsync()
        =>Ok(await _eventsService.GetAllAsync());
    
    [HttpGet("company/{companyId:int}")]
    public async Task<ActionResult<ICollection<GetEventResponse>>> GetAllByCompanyAsync(int companyId) 
        => Ok(await _eventsService.GetByCompanyAsync(companyId));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetEventResponse>> GetByIdAsync(int id)
        =>Ok(await _eventsService.GetByIdAsync(id));
    
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _eventsService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpPost("")]
    [ValidationModel]
    public async Task<ActionResult<GetEventResponse>> CreateAsync([FromBody]CreateEventRequest request)
        => Ok(await _eventsService.CreateAsync(request));

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateAsync(int id, [FromBody]EditEventRequest request)
    {
        await _eventsService.UpdateAsync(id, request);
        return Ok();
    }
}