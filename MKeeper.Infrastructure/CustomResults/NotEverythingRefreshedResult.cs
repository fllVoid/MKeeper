using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Infrastructure.CustomResults;

public class NotEverythingRefreshedResult : SuccessResult
{
}

public class NotEverythingRefreshedResult<T> : SuccessResult<T>
{
    public NotEverythingRefreshedResult(T data) : base(data)
    {
    }
}
