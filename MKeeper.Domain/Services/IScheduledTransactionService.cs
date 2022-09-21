using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface IScheduledTransactionService
{
    Task<Result<int>> Create(ScheduledTransaction transaction);

    Task<Result<ScheduledTransaction[]>> Get(int[] accountIds);

    Task<Result> Update(ScheduledTransaction transaction);

    Task<Result> Delete(int scheduledTransactionId);
}
