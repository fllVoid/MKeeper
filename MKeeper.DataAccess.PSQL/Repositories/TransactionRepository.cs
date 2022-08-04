using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public async Task<int> Add(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction[]> Get(int[] accountIds, int[] categoryIds)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction[]> Get(int[] accountIds)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}
