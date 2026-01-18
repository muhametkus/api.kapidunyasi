using Api.KapiDunyasi.WebAPI.Models;
using FluentValidation;

namespace Api.KapiDunyasi.WebAPI.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).Distinct().ToArray());

            var payload = new ApiErrorResponse
            {
                Type = "https://api.kapidunyasi.com/errors/validation",
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest,
                TraceId = context.TraceIdentifier,
                Errors = errors
            };

            await context.Response.WriteAsJsonAsync(payload);
        }
    }
}
