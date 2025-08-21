namespace Dd.Api.Shared.Application.GlobalResults;

public enum ErrorType {
    BadRequest,
    NotFound,
    ValidationFailure,
    Unauthorized,
    Unauthenticated,
    Unknown,
    Unexpected,
}