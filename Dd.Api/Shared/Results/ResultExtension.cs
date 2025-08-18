namespace Dd.Api.Shared.Results;

public static class ResultExtension {
    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result) {
        return result.IsSuccess switch {
            true => ApiResponse<T>.FromSuccess(result.Value, result.Message),
            _ => ApiResponse<T>.FromFailure(ToApiError(result.ErrorType), result.Message)
        };
    }

    private static ApiError ToApiError(ErrorType? errorType) {
        return errorType switch {
            null => new ApiError("00", "unknown error"),
            ErrorType.NotFound => new ApiError("404", "not found"),
            ErrorType.Unauthorized => new ApiError("401", "unauthorized"),
            ErrorType.Unauthenticated => new ApiError("403", "unauthenticated"),
            ErrorType.ValidationFailure => new ApiError("400", "bad request"),
            ErrorType.Unknown => new ApiError("500", "server error"),
            _ => throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null)
        };
    }
}