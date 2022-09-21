using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidTransferResult : ErrorResult
{
    public InvalidTransferResult(string message) : base(message)
    {
    }

    public InvalidTransferResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidTransferResult<T> : ErrorResult<T>
{
    public InvalidTransferResult(string message) : base(message)
    {
    }

    public InvalidTransferResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
