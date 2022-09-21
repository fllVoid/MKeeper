using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidScheduledTransactionResult : ErrorResult
{
    public InvalidScheduledTransactionResult(string message) : base(message)
    {
    }

    public InvalidScheduledTransactionResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidScheduledTransactionResult<T> : ErrorResult<T>
{
    public InvalidScheduledTransactionResult(string message) : base(message)
    {
    }

    public InvalidScheduledTransactionResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
