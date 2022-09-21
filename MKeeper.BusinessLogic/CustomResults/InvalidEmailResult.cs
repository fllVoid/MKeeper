using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidEmailResult : ErrorResult
{
    public InvalidEmailResult(string message) : base(message)
    {
    }

    public InvalidEmailResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidEmailResult<T> : ErrorResult<T>
{
    public InvalidEmailResult(string message) : base(message)
    {
    }

    public InvalidEmailResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
