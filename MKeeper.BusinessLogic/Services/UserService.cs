using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<int> Create(User user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int userId)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }
}
