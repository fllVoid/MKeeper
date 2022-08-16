using MKeeper.Domain.Exceptions;
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

    public async Task<int> Create(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var isInvalid = string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email);
        if (isInvalid)
        {
            throw new BusinessException(nameof(user));
        }

        var userId = await _userRepository.Add(user);
        return userId;
    }

    public Task<User> Get(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException(nameof(userId));
        }
        return _userRepository.Get(userId);
    }

    public Task<User> Get(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessException(nameof(email));
        }
        return _userRepository.Get(email);
    }

    public async Task Delete(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException(nameof(userId));
        }
        await _userRepository.Delete(userId);
    }

    public async Task Update(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var isInvalid = string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email)
            || user.Id <= 0;
        if (isInvalid)
        {
            throw new BusinessException(nameof(user));
        }
        await _userRepository.Update(user);
    }
}
