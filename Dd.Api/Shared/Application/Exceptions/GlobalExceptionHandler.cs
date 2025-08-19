using System.Text.Json;
using Dd.Api.Shared.Application.Results;
using Microsoft.AspNetCore.Diagnostics;

namespace Dd.Api.Shared.Application.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler {
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken) {
        
        logger.LogError(exception, "an unhandled exception occured {Exception}", exception.Message);
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = exception switch {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var baseResult = BaseResult.Failure(
            exception switch
            {
                BadHttpRequestException => ErrorType.BadRequest,
                _ => ErrorType.Unexpected
            },
            exception.Message
        );

        var json = JsonSerializer.Serialize(baseResult);

        await httpContext.Response.WriteAsync(json, cancellationToken);
        
        return true;
    }
}