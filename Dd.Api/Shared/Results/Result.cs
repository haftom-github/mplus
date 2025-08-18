namespace Dd.Api.Shared.Results;

public class Result<T> {
    public bool IsSuccess { get; }
    public ErrorType? ErrorType { get; }
    public List<Failure>? Errors { get; }
    public string? Message { get; }
    public T? Value { get; }

    private Result(bool isSuccess, T? value, ErrorType? errorType = null, string? message = null) {
        IsSuccess = isSuccess;
        ErrorType = errorType;
        Message = message;
        Value = value;
    }
    
    public static Result<T> Success(T value, string? message = null)
        => new(true, value, message: message);
    
    public static Result<T> Failure(ErrorType errorType, string? message = null)
        => new(false, default, errorType, message);
}

