using System.Net;
using System.Text.Json;
using TpiDSW.Domain.Exceptions;

namespace TpiDSW.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                errorCode = "VALIDATION_ERROR",
                message = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                errorCode = "SERVER_ERROR",
                message = "Ocurrió un error inesperado."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}