using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MKeeperDbContext _context;

    public UserRepository(MKeeperDbContext context) => _context = context;

    public async Task<User> Get(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> Get(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<User[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> Add(User user)
    {
        throw new NotImplementedException();
    }

    public async Task Update(User user)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int userId)
    {
        throw new NotImplementedException();
    }
}
