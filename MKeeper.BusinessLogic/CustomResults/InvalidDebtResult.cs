using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidDebtResult : ErrorResult
{
    public InvalidDebtResult(string message) : base(message)
    {
    }

    public InvalidDebtResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidDebtResult<T> : ErrorResult<T>
{
    public InvalidDebtResult(string message) : base(message)
    {
    }

    public InvalidDebtResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
