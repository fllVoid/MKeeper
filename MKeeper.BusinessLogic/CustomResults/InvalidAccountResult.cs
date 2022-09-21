using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidAccountResult : ErrorResult
{
    public InvalidAccountResult(string message) : base(message)
    {
    }

    public InvalidAccountResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidAccountResult<T> : ErrorResult<T>
{
    public InvalidAccountResult(string message) : base(message)
    {
    }

    public InvalidAccountResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
