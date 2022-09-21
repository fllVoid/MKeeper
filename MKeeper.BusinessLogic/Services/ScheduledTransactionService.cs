using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.BusinessLogic.CustomResults;

namespace MKeeper.BusinessLogic.Services;

public class ScheduledTransactionService : IScheduledTransactionService
{
    private readonly IScheduledTransactionRepository _scheduledTransactionRepository;

    public ScheduledTransactionService(IScheduledTransactionRepository scheduledTransactionRepository)
    {
        _scheduledTransactionRepository = scheduledTransactionRepository;
    }

    public async Task<Result<int>> Create(ScheduledTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }
        var isInvalid = transaction.SourceAccount == null || transaction.Category == null;
        if (isInvalid)
        {
            return new InvalidScheduledTransactionResult<int>(
                $"Invalid ScheduledTransaction object: {transaction}");
        }
        var id = await _scheduledTransactionRepository.Add(transaction);
        return new SuccessResult<int>(id);
    }

    public async Task<Result<ScheduledTransaction[]>> Get(int[] accountIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<ScheduledTransaction[]>("Invalid accountId in accountIds array");
        }
        var scheduledTransactions = await _scheduledTransactionRepository.Get(accountIds);
        return new SuccessResult<ScheduledTransaction[]>(scheduledTransactions);
    }

    public async Task<Result> Update(ScheduledTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }
        var isInvalid = transaction.Id <= 0 || transaction.SourceAccount == null 
            || transaction.Category == null;
        if (isInvalid)
        {
            return new InvalidScheduledTransactionResult($"Invalid ScheduledTransaction object: {transaction}");
        }
        await _scheduledTransactionRepository.Update(transaction);
        return new SuccessResult();
    }

    public async Task<Result> Delete(int scheduledTransactionId)
    {
        if (scheduledTransactionId <= 0)
        {
            return new InvalidIdResult($"Invalid scheduledTransactionId value: {scheduledTransactionId}");
        }
        await _scheduledTransactionRepository.Delete(scheduledTransactionId);
        return new SuccessResult();
    }
}
