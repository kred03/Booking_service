namespace Booking_Service.Application.Common;

public class OperationResult<T>
{
    public bool IsSuccess { get; init; }
    public string? Error { get; init; }
    public T? Value { get; init; }

    public static OperationResult<T> Success(T value) => new()
    {
        IsSuccess = true,
        Value = value
    };

    public static OperationResult<T> Failure(string error) => new()
    {
        IsSuccess = false,
        Error = error
    };
} 