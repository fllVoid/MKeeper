using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class ScheduledTransactionService : IScheduledTransactionService
{
    private readonly IScheduledTransactionRepository _scheduledTransactionRepository;

    public ScheduledTransactionService(IScheduledTransactionRepository scheduledTransactionRepository)
    {
        _scheduledTransactionRepository = scheduledTransactionRepository;
    }

    public async Task<int> Create(ScheduledTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }
        var isInvalid = transaction.SourceAccount == null || transaction.Category == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(transaction));
        }
        return await _scheduledTransactionRepository.Add(transaction);
    }

    public async Task<ScheduledTransaction[]> Get(int[] accountIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(accountIds));
        }
        var scheduledTransactions = await _scheduledTransactionRepository.Get(accountIds);
        return scheduledTransactions;
    }

    public async Task Update(ScheduledTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }
        var isInvalid = transaction.SourceAccount == null || transaction.Category == null;
        if (isInvalid)
        {
            throw new BusinessException(nameof(transaction));
        }
        await _scheduledTransactionRepository.Update(transaction);
    }

    public async Task Delete(int scheduledTransactionId)
    {
        if (scheduledTransactionId <= 0)
        {
            throw new ArgumentException(nameof(scheduledTransactionId));
        }
        await _scheduledTransactionRepository.Delete(scheduledTransactionId);
    }
}
