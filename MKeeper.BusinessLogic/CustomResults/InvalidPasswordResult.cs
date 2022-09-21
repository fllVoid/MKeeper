using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidPasswordResult : ErrorResult
{
    public InvalidPasswordResult(string message) : base(message)
    {
    }

    public InvalidPasswordResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidPasswordResult<T> : ErrorResult<T>
{
    public InvalidPasswordResult(string message) : base(message)
    {
    }

    public InvalidPasswordResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
