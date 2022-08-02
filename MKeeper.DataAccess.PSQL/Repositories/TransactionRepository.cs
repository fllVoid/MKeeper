using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class TransactionRepository : IAbstractTransactionRepository<Transaction>
{
    public Task<int> Add(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> Get(int accountId, int[] categoryIds, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> Get(int accountId, int[] categoryIds)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> Get(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> Get(int[] accountIds, int[] categoryIds)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> Get(int[] accountIds)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Update(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}
