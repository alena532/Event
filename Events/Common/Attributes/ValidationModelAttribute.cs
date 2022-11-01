using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Events.Common.Attributes;

public class ValidationModelAttribute:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];
        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value.ToString().Contains("Request")).Value;

        if (param == null)
        {
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
            return;
        }
        
        if (context.ModelState.IsValid == false)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}