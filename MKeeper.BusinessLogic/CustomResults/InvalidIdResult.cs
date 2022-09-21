using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidIdResult : ErrorResult
{
    public InvalidIdResult(string message) : base(message)
    {
    }

    public InvalidIdResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidIdResult<T> : ErrorResult<T>
{
    public InvalidIdResult(string message) : base(message)
    {
    }

    public InvalidIdResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
