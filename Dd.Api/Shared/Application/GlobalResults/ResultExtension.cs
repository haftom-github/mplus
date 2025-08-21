namespace Dd.Api.Shared.Application.GlobalResults;

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
    
    public static IResult ToHttpResult(this Result result) {
        if (result.IsSuccess) {
            return Results.Ok(result);
        }

        return result.ErrorType switch {
            ErrorType.NotFound => Results.NotFound(result),
            ErrorType.ValidationFailure => Results.BadRequest(result),
            ErrorType.Unauthorized => Results.Unauthorized(),
            _ => Results.InternalServerError(result)
        };
    }

    public static IResult ToHttpResult<T>(this Result<T> result) {
        if (result.IsSuccess) {
            return Results.Ok(result);
        }

        return result.ErrorType switch {
            ErrorType.NotFound => Results.NotFound(result),
            ErrorType.ValidationFailure => Results.BadRequest(result),
            ErrorType.Unauthorized => Results.Unauthorized(),
            _ => Results.InternalServerError(result)
        };
    }
}