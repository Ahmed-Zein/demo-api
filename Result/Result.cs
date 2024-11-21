namespace Result;

public enum ErrorType
{
    Unknown,
    NotFound,
    AlreadyExists,
    InvalidData,
    InternalError,
}

public class Error<TE>(TE data, ErrorType errorType = ErrorType.NotFound)
{
    public TE Data { get; set; } = data;
    public ErrorType Type { get; set; }
}

public class Ok<T>(T data)
{
    public T Data { get; set; } = data;
}

public class Result<T, TE>
{
    private readonly Ok<T>? _ok;
    private readonly Error<TE>? _error;

    public delegate TResult OnOk<out TResult>(T data);

    public delegate TResult OnError<out TResult>(TE data);

    public delegate Task<TResult> OnOkAsync<TResult>(T data);

    public delegate Task<TResult> OnErrorAsync<TResult>(TE data, ErrorType error);

    private Result(Ok<T>? ok, Error<TE>? error)
    {
        if (ok != null && error != null)
            throw new ArgumentException("Cannot have both Ok and Error set.");
        _ok = ok;
        _error = error;
    }

    public TResult Match<TResult>(OnOk<TResult> okFunc, OnError<TResult> errorFunc)
    {
        return _error != null ? errorFunc(_error.Data) : okFunc(_ok!.Data);
    }

    public async Task<TResult> MatchAsync<TResult>(OnOkAsync<TResult> okFunc, OnErrorAsync<TResult> errorFunc)
    {
        return _error != null ? await errorFunc(_error.Data, _error.Type) : await okFunc(_ok!.Data);
    }

    public static Result<T, TE> Success(T data) => new(new Ok<T>(data), null);

    public static Result<T, TE> Error(TE data, ErrorType errorType = ErrorType.Unknown) =>
        new(null, new Error<TE>(data));

    public static Result<T, TE> NotFound(TE data) =>
        new(null, new Error<TE>(data, ErrorType.NotFound));

    public static Result<T, TE> AlreadyExists(TE data) =>
        new(null, new Error<TE>(data, ErrorType.AlreadyExists));

    public static Result<T, TE> InternalError(TE data) =>
        new(null, new Error<TE>(data, ErrorType.InternalError));

    public static Result<T, TE> InvalidData(TE data) =>
        new(null, new Error<TE>(data, ErrorType.InvalidData));
}