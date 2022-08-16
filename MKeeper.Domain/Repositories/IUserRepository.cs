using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IUserRepository
{
    Task<User> Get(int userId);

    Task<User> Get(string email);

    Task<User[]> GetAll();

    Task<int> Add(User user);

    Task Update(User user);

    Task Delete(int userId);
}
