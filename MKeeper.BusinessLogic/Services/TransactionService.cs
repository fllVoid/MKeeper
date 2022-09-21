using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.BusinessLogic.CustomResults;

namespace MKeeper.BusinessLogic.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
		_transactionRepository = transactionRepository;
    }

	public async Task<Result<int>> Create(Transaction transaction)
	{
		if (transaction == null)
        {
			throw new ArgumentNullException(nameof(transaction));
        }
		var isInvalid = transaction.Category == null || transaction.SourceAccount == null;
		if (isInvalid)
        {
			return new InvalidTransactionResult<int>($"Invalid Transaction object:\n{transaction}");
        }
        var id = await _transactionRepository.Add(transaction);
        return new SuccessResult<int>(id);
    }

    public async Task<Result<Transaction[]>> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to)
    {
        if (accountIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transaction[]>("Invalid accountId in accountIds array");
        }
        if (categoryIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transaction[]>("Invalid categoryId in categoryIds array");
        }
        if (from > to)
        {
            return new InvalidDateResult<Transaction[]>($"'from' was less than 'to':\nfrom: {from}\nto: {to}");
        }
        var transactions = await _transactionRepository.Get(accountIds, categoryIds, from, to);
        return new SuccessResult<Transaction[]>(transactions);
    }

    public async Task<Result<Transaction[]>> Get(int[] accountIds, int[] categoryIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transaction[]>("Invalid accountId in accountIds array");
        }
        if (categoryIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transaction[]>("Invalid categoryId in categoryIds array");
        }
        var transactions = await _transactionRepository.Get(accountIds, categoryIds);
        return new SuccessResult<Transaction[]>(transactions);
    }

    public async Task<Result<Transaction[]>> Get(int[] accountIds)
    {
        if (accountIds.Any(x => x <= 0))
        {
            return new InvalidIdResult<Transaction[]>("Invalid accountId in accountIds array");
        }
        var transactions = await _transactionRepository.Get(accountIds);
        return new SuccessResult<Transaction[]>(transactions);
    }

    public async Task<Result> Update(Transaction transaction)
	{
		if (transaction == null)
		{
			throw new ArgumentNullException(nameof(transaction));
		}
		var isInvalid = transaction.Id <= 0 || transaction.Category == null 
            || transaction.SourceAccount == null;
		if (isInvalid)
		{
			return new InvalidTransactionResult($"Invalid Transaction object: {transaction}");
        }
        await _transactionRepository.Update(transaction);
        return new SuccessResult();
    }

    public async Task<Result> Delete(int transactionId)
	{
		if (transactionId <= 0)
        {
			return new InvalidIdResult($"Invalid transactionId value: {transactionId}");
        }
		await _transactionRepository.Delete(transactionId);
        return new SuccessResult();
	}
}
