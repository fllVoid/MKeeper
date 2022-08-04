using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class AccountRepository : IAccountRepository
{
    public async Task<int> Add(Account account)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int accountId)
    {
        throw new NotImplementedException();
    }

    public async Task<Account[]> Get(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Account[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Update(Account account)
    {
        throw new NotImplementedException();
    }
}
