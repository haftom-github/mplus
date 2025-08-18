namespace Dd.Api.Shared.Results;

public class Failure(string code, string message) {
    public string Key { get; } = code;
    public string Message { get; } = message;
}