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

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly AccountService _service;
    private readonly Fixture _fixture;

    public AccountServiceTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _service = new AccountService(_accountRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async void Create_AccountIsValid_ShouldCreateNewAccount()
    {
        //arrange
        var expectedId = _fixture.Create<int>();
        var account = _fixture.Create<Account>();
        _accountRepositoryMock.Setup(x => x.Add(account))
            .ReturnsAsync(expectedId);

        //act
        var actualId = await _service.Create(account);

        //assert
        actualId.Should().Be(expectedId);
        _accountRepositoryMock.Verify(x => x.Add(account), Times.Once);
    }

    [Fact]
    public async void Create_AccountIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _accountRepositoryMock.Verify(x => x.Add(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public async void Create_AccountWithoutUser_ShouldThrowBusinessException()
    {
        //arrange
        var account = _fixture.Build<Account>()
            .Without(x => x.User)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(account));

        //assert
        _accountRepositoryMock.Verify(x => x.Add(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public async void Create_AccountWithoutCurrency_ShouldThrowBusinessException()
    {
        //arrange
        var account = _fixture.Build<Account>()
            .Without(x => x.Currency)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(account));

        //assert
        _accountRepositoryMock.Verify(x => x.Add(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public async void Get_ShouldReturnAccounts()
    {
        //arrange
        var id = _fixture.Create<int>();
        var accounts = new[] { _fixture.Create<Account>() };
        _accountRepositoryMock.Setup(x => x.Get(id))
            .ReturnsAsync(accounts);
        //act
        var result = await _service.Get(id);

        //assert
        Assert.Equal(accounts, result);
        _accountRepositoryMock.Verify(x => x.Get(id), Times.Once);
    }

    [Fact]
    public async void Update_AccountIsValid_ShouldUpdateAccount()
    {
        //arrange
        var initialAccount = _fixture.Create<Account>();
        var changedAccount = _fixture.Build<Account>()
            .With(x => x.Id, initialAccount.Id)
            .With(x => x.User, initialAccount.User)
            .Create();
        _accountRepositoryMock.Setup(x => x.Update(changedAccount))
            .Callback(() =>
            {
                initialAccount.Currency = changedAccount.Currency;
                initialAccount.Balance = changedAccount.Balance;
            });

        //act
        await _service.Update(changedAccount);


        //assert
        Assert.Equal(changedAccount.Balance, initialAccount.Balance);
        Assert.Equal(changedAccount.Currency, initialAccount.Currency);
        Assert.Equal(changedAccount.Id, initialAccount.Id);
        _accountRepositoryMock.Verify(x => x.Update(changedAccount), Times.Once);
    }

    [Fact]
    public async void Update_AccountIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));

        //assert
        _accountRepositoryMock.Verify(x => x.Add(It.IsAny<Account>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public async void Update_AccountIsInvalid_ShouldThrowBusinessException(int accountId)
    {
        //arrange
        var account = _fixture.Build<Account>()
            .With(x => x.Id, accountId)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(account));

        //assert
        _accountRepositoryMock.Verify(x => x.Update(It.IsAny<Account>()), Times.Never);
    }


    [Fact]
    public async void Delete_IdIsValid_ShouldDeleteAccount()
    {
        //arrange
        var id = _fixture.Create<int>();

        //act
        await _service.Delete(id);

        //assert
        _accountRepositoryMock.Verify(x => x.Delete(id), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public async void Delete_IdIsInvalid_ShouldThrowArgumentException(int id)
    {
        //arrange

        //act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.Delete(id));

        //assert
        _accountRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
    }
}
