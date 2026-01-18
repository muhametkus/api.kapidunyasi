using Api.KapiDunyasi.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.KapiDunyasi.WebAPI.Middleware;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        var payload = new ApiErrorResponse
        {
            Type = "https://api.kapidunyasi.com/errors/validation",
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            TraceId = context.HttpContext.TraceIdentifier,
            Errors = errors
        };

        context.Result = new BadRequestObjectResult(payload);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
