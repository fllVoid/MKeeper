using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidUserResult : ErrorResult
{
    public InvalidUserResult(string message) : base(message)
    {
    }

    public InvalidUserResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidUserResult<T> : ErrorResult<T>
{
    public InvalidUserResult(string message) : base(message)
    {
    }

    public InvalidUserResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
