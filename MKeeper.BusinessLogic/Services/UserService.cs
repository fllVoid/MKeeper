using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using System.Security.Cryptography;
using System.Text;
using MKeeper.BusinessLogic.CustomResults;

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
            return new InvalidUserResult<int>($"Invalid username or email:\nusername: '{user.Username}'" +
                $"\nemail: '{user.Email}'");
        }

        var userId = await _userRepository.Add(user);
        return new SuccessResult<int>(userId);
    }

    public async Task<Result<User>> Get(int userId)
    {
        if (userId <= 0)
        {
            return new InvalidIdResult<User>($"Invalid userId value: {userId}");
        }
        var user = await _userRepository.Get(userId);
        return new SuccessResult<User>(user);
    }

    public async Task<Result<User>> Get(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return new InvalidEmailResult<User>($"Invalid user email: '{email}'");
        }
        var user = await _userRepository.Get(email);
        return new SuccessResult<User>(user);
    }

    public async Task<Result> Delete(int userId)
    {
        if (userId <= 0)
        {
            return new InvalidIdResult($"Invalid userId value: {userId}");
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
            return new InvalidUserResult("Invalid User object:\n" +
                user.ToString());
        }
        await _userRepository.Update(user);
        return new SuccessResult();
    }

    public async Task<Result> ChangePassword(int userId, string oldPass, string newPass)
    {
        if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
        {
            return new InvalidPasswordResult("Password was null or white space");
        }
        if (userId <= 0)
        {
            return new InvalidIdResult($"Invalid userId value: {userId}");
        }
        var user = await _userRepository.Get(userId);//TODO check null value
        return await ChangePassword(user, oldPass, newPass);
    }

    public async Task<Result> ChangePassword(string email, string oldPass, string newPass)
    {
        if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
        {
            return new InvalidPasswordResult("Password was null or white space");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            return new InvalidEmailResult($"Invalid email: '{email}'");
        }
        var user = await _userRepository.Get(email);//TODO check null value
        return await ChangePassword(user, oldPass, newPass);
    }

    private async Task<Result> ChangePassword(User user, string oldPass, string newPass)
    {
        var oldPassHash = HashPassword(oldPass);
        if (user.PasswordHash == oldPassHash)
        {
            var newPassHash = HashPassword(newPass);
            user.PasswordHash = newPassHash;
            await _userRepository.Update(user);
            return new SuccessResult();
        }
        return new PasswordDoesntMatchResult("Wrong old password");
    }

    private string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);
        var hashString = Convert.ToHexString(hashBytes);
        return hashString;
    }
}
