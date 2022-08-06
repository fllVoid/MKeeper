using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface IUserService
{
    Task<int> Create(User user);

    Task Update(User user);

    Task Delete(int userId);
}
