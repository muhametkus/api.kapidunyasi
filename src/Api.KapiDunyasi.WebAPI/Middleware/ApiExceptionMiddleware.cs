using Api.KapiDunyasi.WebAPI.Models;

namespace Api.KapiDunyasi.WebAPI.Middleware;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var payload = new ApiErrorResponse
            {
                Type = "https://api.kapidunyasi.com/errors/invalid-operation",
                Title = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                TraceId = context.TraceIdentifier,
                Errors = new Dictionary<string, string[]>()
            };

            await context.Response.WriteAsJsonAsync(payload);
        }
    }
}
