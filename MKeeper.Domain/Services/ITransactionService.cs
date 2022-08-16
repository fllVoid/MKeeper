using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ITransactionService
{
    Task<int> Create(Transaction transaction);

    Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to);

    Task<Transaction[]> Get(int[] accountIds, int[] categoryIds);

    Task<Transaction[]> Get(int[] accountIds);


    Task Update(Transaction transaction);

    Task Delete(int transactionId);
}
