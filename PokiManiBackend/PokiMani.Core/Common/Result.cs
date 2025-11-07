namespace PokiMani.Core.Common;

public class Result<T>
{
    public bool Succeeded { get; }
    public T? Value { get; }
    public string? Error { get; }


    private Result(T? value, string? error, bool succeeded)
    {
        Value = value;
        Error = error;
        Succeeded = succeeded;
    }

    public static Result<T> Success(T value) => new(value, null, true);

    public static Result<T> Failure(string error) => new(default, error, false);
}
