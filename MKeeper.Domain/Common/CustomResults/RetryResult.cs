namespace MKeeper.Domain.Common.CustomResults;

public class RetryResult : ErrorResult
{
    public RetryResult(string message) : base(message)
    {
    }

    public RetryResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {

    }
}

public class RetryResult<T> : ErrorResult<T>
{
    public RetryResult(string message) : base(message)
    {
    }

    public RetryResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {

    }
}