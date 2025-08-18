namespace Dd.Api.Shared.Results;

public static class ResultExtension {
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result) {
        return result.IsSuccess switch {
            true => ApiResponse<T>.FromSuccess(result.Value, result.Message),
            _ => ApiResponse<T>.FromFailure(GetStatus(result.ErrorType), result.Errors, GetMessage(result.ErrorType))
        };
    }

    private static int GetStatus(ErrorType? errorType) {
        return errorType switch {
            null => 500,
            ErrorType.NotFound => 404,
            ErrorType.Unauthorized => 401,
            ErrorType.Unauthenticated => 403,
            ErrorType.ValidationFailure => 400,
            ErrorType.Unknown => 500,
            _ => throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null)
        };
    }
    
    private static string GetMessage(ErrorType? errorType) {
        return errorType switch {
            null => "Internal Server Error",
            ErrorType.NotFound => "Not Found",
            ErrorType.Unauthorized => "UnAuthorized",
            ErrorType.Unauthenticated => "UnAuthenticated",
            ErrorType.ValidationFailure => "Bad Request",
            ErrorType.Unknown => "Internal Server Error",
            _ => throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null)
        };
    }
}