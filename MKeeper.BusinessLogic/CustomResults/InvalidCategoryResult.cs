using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.BusinessLogic.CustomResults;

public class InvalidCategoryResult : ErrorResult
{
    public InvalidCategoryResult(string message) : base(message)
    {
    }

    public InvalidCategoryResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}

public class InvalidCategoryResult<T> : ErrorResult<T>
{
    public InvalidCategoryResult(string message) : base(message)
    {
    }

    public InvalidCategoryResult(string message, IReadOnlyCollection<Error> errors) : base(message, errors)
    {
    }
}
