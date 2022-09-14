using MKeeper.BusinessLogic.Services;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Models;
using Moq;
using Xunit;
using AutoFixture;
using FluentAssertions;
using System;
using MKeeper.Domain.Exceptions;

namespace MKeeper.BusinessLogic.XTests.ServiceTests;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly TransactionService _service;
	private readonly Fixture _fixture;

    public TransactionServiceTests()
	{
		_transactionRepositoryMock = new Mock<ITransactionRepository>();
		_service = new TransactionService(_transactionRepositoryMock.Object);
		_fixture = new Fixture();
	}

    [Fact]
    public async void Create_TransactionIsValid_ShouldCreateNewTransaction()
    {
        //arrange
        var expectedId = _fixture.Create<int>();
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transaction = _fixture.Build<Transaction>()
            .With(x => x.Category, category)
            .Create();
        _transactionRepositoryMock.Setup(x => x.Add(transaction))
            .ReturnsAsync(expectedId);

        //act
        var actualId = await _service.Create(transaction);

        //assert
        actualId.Should().Be(expectedId);
        _transactionRepositoryMock.Verify(x => x.Add(transaction), Times.Once);
    }

    [Fact]
    public async void Create_TransactionIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _transactionRepositoryMock.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async void Create_TransactionWithoutCategory_ShouldThrowBusinessException()
    {
        //arrange
        var transaction = _fixture.Build<Transaction>()
            .Without(x => x.Category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(transaction));

        //assert
        _transactionRepositoryMock.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async void Create_TransactionWithoutAccount_ShouldThrowBusinessException()
    {
        //arrange
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transaction = _fixture.Build<Transaction>()
            .Without(x => x.SourceAccount)
            .With(x => x.Category, category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(transaction));

        //assert
        _transactionRepositoryMock.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async void Get_ByAccountIds_ShouldReturnTransactions()
    {
        //arrange
        var accountIds = new[] { _fixture.Create<int>() };
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transactions = new[] { _fixture.Build<Transaction>().With(x => x.Category, category).Create() };
        _transactionRepositoryMock.Setup(x => x.Get(accountIds))
            .ReturnsAsync(transactions);
        //act
        var result = await _service.Get(accountIds);

        //assert
        Assert.Equal(transactions, result);
        _transactionRepositoryMock.Verify(x => x.Get(accountIds), Times.Once);
    }

    [Fact]
    public async void Get_ByAccountIdsAndCategoryIds_ShouldReturnTransactions()
    {
        //arrange
        var accountIds = new[] { _fixture.Create<int>() };
        var categoryIds = new[] { _fixture.Create<int>() };
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transactions = new[] { _fixture.Build<Transaction>().With(x => x.Category, category).Create() };
        _transactionRepositoryMock.Setup(x => x.Get(accountIds, categoryIds))
            .ReturnsAsync(transactions);
        //act
        var result = await _service.Get(accountIds, categoryIds);

        //assert
        Assert.Equal(transactions, result);
        _transactionRepositoryMock.Verify(x => x.Get(accountIds, categoryIds), Times.Once);
    }

    [Fact]
    public async void Get_ByAccountIdsAndCategoryIdsAndDate_ShouldReturnTransactions()
    {
        //arrange
        var accountIds = new[] { _fixture.Create<int>() };
        var categoryIds = new[] { _fixture.Create<int>() };
        var from = _fixture.Create<DateTime>();
        var to = from.AddDays(1);
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transactions = new[] { _fixture.Build<Transaction>().With(x => x.Category, category).Create() };
        _transactionRepositoryMock.Setup(x => x.Get(accountIds, categoryIds, from, to))
            .ReturnsAsync(transactions);
        //act
        var result = await _service.Get(accountIds, categoryIds, from, to);

        //assert
        Assert.Equal(transactions, result);
        _transactionRepositoryMock.Verify(x => x.Get(accountIds, categoryIds, from, to), Times.Once);
    }

    [Fact]
    public async void Update_TransactionIsValid_ShouldUpdateTransaction()
    {
        //arrange
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var anotherCategory = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var initialTransaction = _fixture.Build<Transaction>()
            .With(x => x.Category, category)
            .Create();
        var changedTransaction = _fixture.Build<Transaction>()
            .With(x => x.Category, anotherCategory)
            .Create();
        _transactionRepositoryMock.Setup(x => x.Update(changedTransaction))
            .Callback(() =>
            {
                initialTransaction.Sum = changedTransaction.Sum;
                initialTransaction.CreationDate = changedTransaction.CreationDate;
                initialTransaction.Category = changedTransaction.Category;
                initialTransaction.SourceAccount = changedTransaction.SourceAccount;
                initialTransaction.Comment = changedTransaction.Comment;
            });

        //act
        await _service.Update(changedTransaction);

        //assert
        Assert.Equal(changedTransaction.Sum, initialTransaction.Sum);
        Assert.Equal(changedTransaction.CreationDate, initialTransaction.CreationDate);
        Assert.Equal(changedTransaction.Category, initialTransaction.Category);
        Assert.Equal(changedTransaction.SourceAccount, initialTransaction.SourceAccount);
        Assert.Equal(changedTransaction.Comment, initialTransaction.Comment);
        _transactionRepositoryMock.Verify(x => x.Update(changedTransaction), Times.Once);
    }

    [Fact]
    public async void Update_TransactionIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));

        //assert
        _transactionRepositoryMock.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async void Update_TransactionWithoutAccount_ShouldThrowBusinessException()
    {
        //arrange
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transaction = _fixture.Build<Transaction>()
            .Without(x => x.SourceAccount)
            .With(x => x.Category, category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(transaction));

        //assert
        _transactionRepositoryMock.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async void Update_TransactionWithoutCategory_ShouldThrowBusinessException()
    {
        //arrange
        var transaction = _fixture.Build<Transaction>()
            .Without(x => x.Category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(transaction));

        //assert
        _transactionRepositoryMock.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Never);
    }

    [Fact]
    public async void Delete_IdIsValid_ShouldDeleteTransaction()
    {
        //arrange
        var id = _fixture.Create<int>();

        //act
        await _service.Delete(id);

        //assert
        _transactionRepositoryMock.Verify(x => x.Delete(id), Times.Once);
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
        _transactionRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
    }
}
