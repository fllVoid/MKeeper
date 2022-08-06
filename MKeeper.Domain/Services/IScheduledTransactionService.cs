using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface IScheduledTransactionService
{
    Task<int> Create(ScheduledTransaction transaction);

    Task Update(ScheduledTransaction transaction);

    Task Delete(ScheduledTransaction transaction);
}
