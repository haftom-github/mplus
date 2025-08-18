using Microsoft.AspNetCore.Diagnostics;

namespace Dd.Api.Shared.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler {
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken) {
        
        logger.LogError(exception, "an unhandled exception occured {Exception}", exception.Message);
        
        throw new NotImplementedException();
    }
}