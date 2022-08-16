using MKeeper.BusinessLogic.Services;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Models;
using Moq;
using Xunit;
using AutoFixture;
using FluentAssertions;
using System;
using MKeeper.Domain.Exceptions;

namespace MKeeper.BusinessLogic.XTests;

public class UserServiceTests
{
	private readonly Mock<IUserRepository> _userRepositoryMock;
	private readonly UserService _service;
	private readonly Fixture _fixture;

	public UserServiceTests()
	{
		_userRepositoryMock = new Mock<IUserRepository>();
		_service = new UserService(_userRepositoryMock.Object);
		_fixture = new Fixture();
	}

	[Fact]
	public async void Create_UserIsValid_ShouldCreateNewUser()
	{
		//arrange
		var expectedUserId = _fixture.Create<int>();
		var user = _fixture.Create<User>();
		_userRepositoryMock.Setup(x => x.Add(user))
			.ReturnsAsync(expectedUserId);

		//act
		var result = await _service.Create(user);

		//assert
		result.Should().Be(expectedUserId);
		_userRepositoryMock.Verify(x => x.Add(user), Times.Once);
	}

	[Fact]
	public async void Create_UserIsNull_ShouldThrowArgumentNullException()
	{
		User user = null;
		//_userRespositoryMock.Setup(x => x.Add(user)).ThrowsAsync<>

		//act
		var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(user));

		//assert
		_userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
	}

	[Theory]
	[InlineData(null, null)]
	[InlineData("", "")]
	[InlineData("test username", "")]
	[InlineData("test username", null)]
	[InlineData("", "test email")]
	[InlineData(null, "test email")]
	public async void Create_UserIsInvalid_ShouldThrowBusinessException(string username, string email)
	{
		//arrange
		var user = _fixture.Build<User>()
			.With(x => x.Username, username)
			.With(x => x.Email, email)
			.Create();

		//act
		var ex = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(user));

		//assert
		_userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
	}

	[Fact]
	public async void Get_ById_ShouldReturnUsers()
	{
		//arrange
		var userId = _fixture.Create<int>();
		var user = _fixture.Build<User>()
			.With(x => x.Id, userId)
			.Create();
		_userRepositoryMock.Setup(x => x.Get(userId))
			.ReturnsAsync(user);
		//act
		var result = await _service.Get(userId);

		//assert
		Assert.Equal(user, result);
		_userRepositoryMock.Verify(x => x.Get(userId), Times.Once);
	}

	[Fact]
	public async void Get_ByEmail_ShouldReturnUsers()
	{
		//arrange
		var userEmail = _fixture.Create<string>();
		var user = _fixture.Build<User>()
			.With(x => x.Email, userEmail)
			.Create();
		_userRepositoryMock.Setup(x => x.Get(userEmail))
			.ReturnsAsync(user);
		//act
		var result = await _service.Get(userEmail);

		//assert
		Assert.Equal(user, result);
		_userRepositoryMock.Verify(x => x.Get(userEmail), Times.Once);
	}

	[Fact]
	public async void Update_UserIsValid_ShouldUpdateUser()
	{
		//arrange
		var id = _fixture.Create<int>();
		var initialUser = _fixture.Build<User>()
			.With(x => x.Id, id)
			.Create();
		var changedUser = _fixture.Build<User>()
			.With(x => x.Id, id)
			.Create();
		_userRepositoryMock
			.Setup(x => x.Update(changedUser))
			.Callback(() =>
			{
				initialUser.Username = changedUser.Username;
				initialUser.Email = changedUser.Email;
			});

		//act
		await _service.Update(changedUser);

		//assert
		_userRepositoryMock.Verify(x => x.Update(changedUser), Times.Once);
		Assert.Equal(changedUser.Username, initialUser.Username);
		Assert.Equal(changedUser.Email, initialUser.Email);
	}

	[Fact]
	public async void Update_UserIsNull_ShouldReturnArgumentNullException()
	{
		User user = null;
		//_userRespositoryMock.Setup(x => x.Add(user)).ThrowsAsync<>

		//act
		var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(user));

		//assert
		_userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
	}

	[Theory]
	[InlineData(null, null)]
	[InlineData("", "")]
	[InlineData("test username", "")]
	[InlineData("test username", null)]
	[InlineData("", "test email")]
	[InlineData(null, "test email")]
	public async void Update_UserIsInvalid_ShouldReturnBusinessException(string username, string email)
	{
		//arrange
		var user = _fixture.Build<User>()
			.With(x => x.Username, username)
			.With(x => x.Email, email)
			.Create();

		//act
		var ex = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(user));

		//assert
		_userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
	}

	[Fact]
	public async void Delete_IdIsValid_ShouldDeleteUser()
    {
		//arrange
		var id = _fixture.Create<int>();

		//act
		await _service.Delete(id);

		//assert
		_userRepositoryMock.Verify(x => x.Delete(id), Times.Once);
	}

	[Theory]
	[InlineData(default(int))]
	[InlineData(int.MinValue)]
	public async void Delete_IdIsInvalid_ShouldThrowArgumentException(int id)
	{
		//arrange

		//act
		var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.Delete(id));

		//assert
		_userRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
	}
}
