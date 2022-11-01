using Events.Contracts.Responses.Speakers;
using Events.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers.Admin;


[ApiController] 
[Authorize(Roles="Admin")]
[Route("api/admin/[controller]")]
public class SpeakersController:ControllerBase
{
    private readonly ISpeakersService _speakersService;
    
    public SpeakersController(ISpeakersService speakersService)
    {
        _speakersService = speakersService;
    }
    
    
    [HttpGet("")]
       
    public async Task<ActionResult<ICollection<GetSpeakerResponse>>> GetAllAsync()
    {
            
        return Ok(await _speakersService.GetAllAsync());
    }
}