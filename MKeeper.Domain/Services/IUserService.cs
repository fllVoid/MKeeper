using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface IUserService
{
    Task<Result<int>> Create(User user);

    Task<Result<User>> Get(int userId);

    Task<Result<User>> Get(string email);

    Task<Result> Update(User user);

    Task<Result> Delete(int userId);

    Task<Result> ChangePassword(int userId, string oldPass, string newPass);

    Task<Result> ChangePassword(string userLogin, string oldPass, string newPass);
}
