using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using System.Security.Cryptography;
using System.Text;

namespace MKeeper.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Create(User user)
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
        return new SuccessResult<int>(userId);
    }

    public async Task<Result<User>> Get(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException(nameof(userId));
        }
        var user = await _userRepository.Get(userId);
        return new SuccessResult<User>(user);
    }

    public async Task<Result<User>> Get(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessException(nameof(email));
        }
        var user = await _userRepository.Get(email);
        return new SuccessResult<User>(user);
    }

    public async Task<Result> Delete(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException(nameof(userId));
        }
        await _userRepository.Delete(userId);
        return new SuccessResult();
    }

    public async Task<Result> Update(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var isInvalid = string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email)
            || user.Id <= 0;
        if (isInvalid)
        {
            //TODO maybe change to kind of EntitiyNotValidResult
            throw new BusinessException(nameof(user));
        }
        await _userRepository.Update(user);
        return new SuccessResult();
    }

    public async Task<Result> ChangePassword(int userId, string oldPass, string newPass)
    {
        if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
        {
            throw new ArgumentException("Password was null or white space");
        }
        if (userId <= 0)
        {
            throw new ArgumentException($"Invalid userId value: {userId}");
        }
        var user = await _userRepository.Get(userId);//TODO check null value
        return await ChangePassword(user, oldPass, newPass);
    }

    public async Task<Result> ChangePassword(string userLogin, string oldPass, string newPass)
    {
        if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
        {
            throw new ArgumentException("Password was null or white space");
        }
        if (string.IsNullOrWhiteSpace(userLogin))
        {
            throw new ArgumentException($"Invalid login value: {userLogin}");
        }
        var user = await _userRepository.Get(userLogin);//TODO check null value
        return await ChangePassword(user, oldPass, newPass);
    }

    private async Task<Result> ChangePassword(User user, string oldPass, string newPass)
    {
        var oldPassHash = HashPassword(HashPassword(oldPass));
        if (user.PasswordHash == oldPassHash)
        {
            var newPassHash = HashPassword(newPass);
            user.PasswordHash = newPassHash;
            await _userRepository.Update(user);
            return new SuccessResult();
        }
        return new ErrorResult("Wrong old password");
    }

    private string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);
        var hashString = Convert.ToHexString(hashBytes);
        return hashString;
    }
}
