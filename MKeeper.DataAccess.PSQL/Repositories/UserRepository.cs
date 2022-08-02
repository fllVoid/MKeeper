using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User> Get(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<User[]> GetAll()
    {
        throw new NotImplementedException();
    }
    public Task<int> Add(User user)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int userId)
    {
        throw new NotImplementedException();
    }
}
