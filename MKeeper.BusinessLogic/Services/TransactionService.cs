using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
		_transactionRepository = transactionRepository;
    }

	public async Task<int> Create(Transaction transaction)
	{
		if (transaction == null)
        {
			throw new ArgumentNullException(nameof(transaction));
        }
		var isInvalid = transaction.Category == null || transaction.SourceAccount == null;
		if (isInvalid)
        {
			throw new BusinessException(nameof(transaction));
        }
		return await _transactionRepository.Add(transaction);
    }

    public async Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to)
    {
        if (accountIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(accountIds));
        }
        if (categoryIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(categoryIds));
        }
        if (from > to)
        {
            throw new BusinessException(nameof(to));
        }
        var transactions = await _transactionRepository.Get(accountIds, categoryIds, from, to);
        return transactions;
    }

    public async Task<Transaction[]> Get(int[] accountIds, int[] categoryIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(accountIds));
        }
        if (categoryIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(categoryIds));
        }
        var transactions = await _transactionRepository.Get(accountIds, categoryIds);
        return transactions;
    }

    public async Task<Transaction[]> Get(int[] accountIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            throw new ArgumentException(nameof(accountIds));
        }
        var transactions = await _transactionRepository.Get(accountIds);
        return transactions;
    }

    public async Task Update(Transaction transaction)
	{
		if (transaction == null)
		{
			throw new ArgumentNullException(nameof(transaction));
		}
		var isInvalid = transaction.Category == null || transaction.SourceAccount == null;
		if (isInvalid)
		{
			throw new BusinessException(nameof(transaction));
		}
		await _transactionRepository.Update(transaction);
	}

	public async Task Delete(int transactionId)
	{
		if (transactionId <= 0)
        {
			throw new ArgumentException(nameof(transactionId));
        }
		await _transactionRepository.Delete(transactionId);
	}
}
