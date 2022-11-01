using Events.Contracts.Responses.Companies;
using Events.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers.Admin;

[ApiController] 
[Authorize(Roles="Admin")]
[Route("api/admin/[controller]")]
public class CompaniesController:ControllerBase
{
    private readonly ICompaniesService _companiesService;
    
    public CompaniesController(ICompaniesService companiesService)
    {
        _companiesService = companiesService;
    }
    
    
    [HttpGet("")]
       
    public async Task<ActionResult<ICollection<GetCompanyResponse>>> GetAllAsync()
    {
            
        return Ok(await _companiesService.GetAllAsync());
    }
        
       
}
