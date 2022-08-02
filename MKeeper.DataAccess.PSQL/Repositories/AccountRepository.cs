using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task<Account[]> GetAccounts(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Account[]> GetAllAccounts()
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAccount(Account account)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAccount(Account account)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAccount(int accountId)
    {
        throw new NotImplementedException();
    }
}
