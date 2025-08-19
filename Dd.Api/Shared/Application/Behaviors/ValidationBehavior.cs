using Dd.Api.Shared.Application.Results;
using FluentValidation;
using MediatR;

namespace Dd.Api.Shared.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : BaseResult {
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken) {
        if (!validators.Any()) return await next(cancellationToken);
        var context = new ValidationContext<TRequest>(request);

        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (failures.Count == 0) return await next(cancellationToken);

        var validationFailures = failures
            .Select(failure => new Failure(failure.PropertyName, failure.ErrorMessage))
            .ToList();

        return BaseResult.FailureAs<TResponse>(
            ErrorType.ValidationFailure,
            validationFailures,
            "Validation failed for one or more requests.");
    }
}