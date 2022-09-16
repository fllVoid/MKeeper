using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface IUserRepository
{
    Task<User> Get(int userId, CancellationToken cancellationToken = default);

    Task<User> Get(string email, CancellationToken cancellationToken = default);

    Task<User[]> GetAll(CancellationToken cancellationToken = default);

    Task<int> Add(User user, CancellationToken cancellationToken = default);

    Task Update(User user, CancellationToken cancellationToken = default);

    Task Delete(int userId, CancellationToken cancellationToken = default);
}
