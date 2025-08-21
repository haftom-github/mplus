namespace Dd.Api.Shared.Application.GlobalResults;

public class ApiResponse<T> {
    public int StatusCode { get; set; }
    public bool IsSuccess { get; }
    public T? Data { get; }
    public List<Failure>? Errors { get; }
    public string? Message { get; }

    private ApiResponse(int statusCode, bool isSuccess, T? data, string? message, List<Failure>? errors) {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public static ApiResponse<T> FromSuccess(T? data = default, string? message = null, int statusCode = 200) 
        => new(statusCode, true, data, message, null);

    public static ApiResponse<T> FromFailure(int statusCode, List<Failure>? apiError, string? message) 
        => new(statusCode, false, default, message, apiError);
}