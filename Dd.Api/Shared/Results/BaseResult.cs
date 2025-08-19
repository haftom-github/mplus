namespace Dd.Api.Shared.Results;

public class BaseResult {
    public bool IsSuccess { get; protected set; }
    public ErrorType? ErrorType { get; protected set; }
    public List<Failure>? Errors { get; protected set; }
    public string? Message { get; protected set; }
    
    protected BaseResult(bool isSuccess, ErrorType? errorType = null, List<Failure>? failures = null, string? message = null) {
        IsSuccess = isSuccess;
        ErrorType = errorType;
        Errors = failures;
        Message = message;
    }
    
    public static BaseResult Success(string? message = null)
        => new(true, message: message);
    
    public new static BaseResult Failure(ErrorType errorType, string? message = null)
        => new(false, errorType, message: message);
    
    public new static BaseResult Failure(ErrorType errorType, List<Failure> failures, string? message = null)
        => new(false, errorType, failures, message);
    
    public static T FailureAs<T>(ErrorType errorType, string? message = null) where T : BaseResult {
        return typeof(T)
               .GetMethod("Failure", [typeof(ErrorType), typeof(string)])
               ?.Invoke(null, [errorType, message]) as T
           ?? throw new InvalidOperationException();
    }
    
    public static T FailureAs<T>(ErrorType errorType, List<Failure> failures, string? message = null) where T : BaseResult {
        return typeof(T)
                   .GetMethod("Failure", [typeof(ErrorType), typeof(List<Failure>), typeof(string)])
                   ?.Invoke(null, [errorType, failures, message]) as T
               ?? throw new InvalidOperationException();
    }
}