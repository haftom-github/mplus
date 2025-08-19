using MediatR;

namespace Dd.Api.Shared.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull {
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken) {
        
        logger.LogInformation("handling {RequestName} with data {@Request}", typeof(TRequest).Name, request);

        var response = await next(cancellationToken);
        
        logger.LogInformation("handled {RequestName} with {@Response}", typeof(TRequest).Name, response);
        
        return response;
    }
}