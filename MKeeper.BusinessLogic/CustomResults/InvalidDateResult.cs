using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidDateResult : ErrorResult
{
    public InvalidDateResult(string message) : base(message)
    {
    }

    public InvalidDateResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidDateResult<T> : ErrorResult<T>
{
    public InvalidDateResult(string message) : base(message)
    {
    }

    public InvalidDateResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
