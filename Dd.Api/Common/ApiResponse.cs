namespace Dd.Api.Common;

public class ApiResponse<T> {
    public bool IsSuccess { get; }
    public T? Data { get; }
    public ApiError? ApiError { get; }
    public string? Message { get; }

    private ApiResponse(bool isSuccess, T? data, string? message, ApiError? apiError) {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        ApiError = apiError;
    }

    public static ApiResponse<T> FromSuccess(T? data, string? message) 
        => new(true, data, message, null);

    public static ApiResponse<T> FromFailure(ApiError apiError, string? message) 
        => new(false, default, message, apiError);
}

public class ApiError(string code, string message) {
    public string Code { get; } = code;
    public string Message { get; } = message;
}