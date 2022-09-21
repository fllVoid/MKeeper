using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidTransactionResult : ErrorResult
{
    public InvalidTransactionResult(string message) : base(message)
    {
    }

    public InvalidTransactionResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidTransactionResult<T> : ErrorResult<T>
{
    public InvalidTransactionResult(string message) : base(message)
    {
    }

    public InvalidTransactionResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
