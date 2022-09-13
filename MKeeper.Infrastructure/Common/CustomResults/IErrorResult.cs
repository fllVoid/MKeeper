
namespace MKeeper.Infrastructure.Common.CustomResults;

internal interface IErrorResult
{
    string Message { get; }
    IReadOnlyCollection<Error> Errors { get; }
}
