using Events.DBContext.Repositories;
using Events.DBContext.Repositories.Base;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Events.Common.Attributes;

public class ValidationEventAttribute:IAsyncActionFilter
{
    private readonly IRepository<Event> _repository;
    public ValidationEventAttribute(IRepository<Event> repository)
    {
        _repository = repository;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
    {
        //var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
        int id = Convert.ToInt32(context.ActionArguments["id"]);
        var checkEvent = await _repository.GetByIdAsync(id);
        if (checkEvent == null)
        {
            context.Result = new BadRequestObjectResult("Invalid Event Id");
        }
        else
        {
            await next();
        }
        
    }
    
}