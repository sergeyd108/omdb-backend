using System.Net;
using System.Text.Json;

namespace omdb_backend;

public class HttpErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (HttpRequestException exception)
        {
            var errorResponse = new
            {
                message = "An error occurred while processing your request.",
                details = exception.Message
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)(exception.StatusCode ?? HttpStatusCode.BadRequest);

            await context.Response.WriteAsync(errorJson);
        }
    }
}