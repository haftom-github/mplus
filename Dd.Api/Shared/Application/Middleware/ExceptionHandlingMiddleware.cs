using System.Text.Json;
using Dd.Api.Shared.Application.Results;

namespace Dd.Api.Shared.Application.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next) {
    public async Task InvokeAsync(HttpContext context) {
        try {
            await next(context);
        }
        catch (Exception e) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = e switch {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var baseResult = BaseResult.Failure(
                e switch {
                    BadHttpRequestException => ErrorType.BadRequest,
                    _ => ErrorType.Unexpected
                },
                e.Message
            );

            var json = JsonSerializer.Serialize(baseResult);

            await context.Response.WriteAsync(json);
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions {
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}