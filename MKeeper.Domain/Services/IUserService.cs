using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface IUserService
{
    Task<int> Create(User user);

    Task<User> Get(int userId);

    Task<User> Get(string email);

    Task Update(User user);

    Task Delete(int userId);
}
