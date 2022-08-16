using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface IScheduledTransactionService
{
    Task<int> Create(ScheduledTransaction transaction);

    Task<ScheduledTransaction[]> Get(int[] accountIds);

    Task Update(ScheduledTransaction transaction);

    Task Delete(int scheduledTransactionId);
}
