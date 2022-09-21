using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface ITransactionService
{
    Task<Result<int>> Create(Transaction transaction);

    Task<Result<Transaction[]>> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to);

    Task<Result<Transaction[]>> Get(int[] accountIds, int[] categoryIds);

    Task<Result<Transaction[]>> Get(int[] accountIds);

    Task<Result> Update(Transaction transaction);

    Task<Result> Delete(int transactionId);
}
