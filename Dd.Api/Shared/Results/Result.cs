namespace Dd.Api.Shared.Results;

public class Result : BaseResult {
    
    private Result(bool isSuccess, ErrorType? errorType = null, List<Failure>? failures = null, string? message = null) 
        : base(isSuccess, errorType, failures, message){}
}

public class Result<T> : BaseResult {
    public T? Value { get; }

    private Result(bool isSuccess, T? value, ErrorType? errorType = null, List<Failure>? failures = null, string? message = null) : base(isSuccess, errorType, failures, message) {
        Value = value;
    }
    
    public static Result<T> Success(T value, string? message = null)
        => new(true, value, message: message);
    
    public new static Result<T> Failure(ErrorType errorType, string? message = null)
        => new(false, default, errorType, message: message);
    
    public new static Result<T> Failure(ErrorType errorType, List<Failure> failures, string? message = null)
        => new(false, default, errorType, failures, message);
}




