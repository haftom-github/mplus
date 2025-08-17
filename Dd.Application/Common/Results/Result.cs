namespace Dd.Application.Common.Results;

public class Result {
    public bool IsSuccess { get; }
    public ErrorType? ErrorType { get; }
    public string? Message { get; }

    protected Result(bool isSuccess, ErrorType? errorType = null, string? message = null) {
        IsSuccess = isSuccess;
        ErrorType = errorType;
        Message = message;
    }

    public static Result Success(string? message = null) 
        => new(true, message: message);
    
    public static Result Failure(ErrorType errorType, string? message = null) 
        => new (false, errorType, message);
}

public class Result<T> {
    public bool IsSuccess { get; }
    public ErrorType? ErrorType { get; }
    public string? Message { get; }
    T? Value { get; }
    protected Result(bool isSuccess, T? value, ErrorType? errorType = null, string? message = null) {
        IsSuccess = isSuccess;
        ErrorType = errorType;
        Message = message;
        Value = value;
    }
    
    public static Result<T> Success(T value, string? message = null)
        => new(true, value, message: message);
    
    public new static Result<T> Failure(ErrorType errorType, string? message = null)
        => new(false, default, errorType, message);
}
