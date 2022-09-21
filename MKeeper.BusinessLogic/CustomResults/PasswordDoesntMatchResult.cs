using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class PasswordDoesntMatchResult : ErrorResult
{
    public PasswordDoesntMatchResult(string message) : base(message)
    {
    }

    public PasswordDoesntMatchResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class PasswordDoesntMatchResult<T> : ErrorResult<T>
{
    public PasswordDoesntMatchResult(string message) : base(message)
    {
    }

    public PasswordDoesntMatchResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
