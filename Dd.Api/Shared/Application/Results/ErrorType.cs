namespace Dd.Api.Shared.Application.Results;

public enum ErrorType {
    BadRequest,
    NotFound,
    ValidationFailure,
    Unauthorized,
    Unauthenticated,
    Unknown,
    Unexpected,
}