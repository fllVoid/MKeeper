using MKeeper.Domain.Models;

namespace MKeeper.Domain;

public interface IUserRepository
{
    User GetUserById(int userId);
    IEnumerable<User> GetAll();
}
