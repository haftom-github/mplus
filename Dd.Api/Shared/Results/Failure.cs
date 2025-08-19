namespace Dd.Api.Shared.Results;

public class Failure(string key, string message) {
    public string Key { get; } = key;
    public string Message { get; } = message;
}