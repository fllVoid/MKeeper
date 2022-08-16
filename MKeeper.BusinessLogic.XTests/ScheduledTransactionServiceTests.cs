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

public class ScheduledTransactionServiceTests
{
    private readonly Mock<IScheduledTransactionRepository> _scheduledTransactionRepoMock;
    private readonly ScheduledTransactionService _service;
    private readonly Fixture _fixture;

    public ScheduledTransactionServiceTests()
    {
        _scheduledTransactionRepoMock = new Mock<IScheduledTransactionRepository>();
        _service = new ScheduledTransactionService(_scheduledTransactionRepoMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async void Create_ScheduledTransactionIsValid_ShouldCreateNewTransaction()
    {
        //arrange
        var expectedId = _fixture.Create<int>();
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var scheduledTransaction = _fixture.Build<ScheduledTransaction>()
            .With(x => x.Category, category)
            .Create();
        _scheduledTransactionRepoMock.Setup(x => x.Add(scheduledTransaction))
            .ReturnsAsync(expectedId);

        //act
        var actualId = await _service.Create(scheduledTransaction);

        //assert
        actualId.Should().Be(expectedId);
        _scheduledTransactionRepoMock.Verify(x => x.Add(scheduledTransaction), Times.Once);
    }

    [Fact]
    public async void Create_ScheduledTransactionIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Add(It.IsAny<ScheduledTransaction>()), Times.Never);
    }

    [Fact]
    public async void Create_ScheduledTransactionWithoutCategory_ShouldThrowBusinessException()
    {
        //arrange
        var scheduledTransaction = _fixture.Build<ScheduledTransaction>()
            .Without(x => x.Category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(scheduledTransaction));

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Add(It.IsAny<ScheduledTransaction>()), Times.Never);
    }

    [Fact]
    public async void Create_ScheduledTransactionWithoutAccount_ShouldThrowBusinessException()
    {
        //arrange
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var scheduledTransaction = _fixture.Build<ScheduledTransaction>()
            .Without(x => x.SourceAccount)
            .With(x => x.Category, category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(scheduledTransaction));

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Add(It.IsAny<ScheduledTransaction>()), Times.Never);
    }

    [Fact]
    public async void Get_ShouldReturnScheduledTransactions()
    {
        //arrange
        var accountIds = new[] { _fixture.Create<int>() };
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var transactions = new[] { _fixture.Build<ScheduledTransaction>().With(x => x.Category, category).Create() };
        _scheduledTransactionRepoMock.Setup(x => x.Get(accountIds))
            .ReturnsAsync(transactions);
        //act
        var result = await _service.Get(accountIds);

        //assert
        Assert.Equal(transactions, result);
        _scheduledTransactionRepoMock.Verify(x => x.Get(accountIds), Times.Once);
    }

    [Fact]
    public async void Update_ScheduledTransactionIsValid_ShouldUpdateTransaction()
    {
        //arrange
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var changedTransaction = _fixture.Build<ScheduledTransaction>()
            .With(x => x.Category, category)
            .Create();

        //act
        await _service.Update(changedTransaction);

        //asserе
        _scheduledTransactionRepoMock.Verify(x => x.Update(changedTransaction), Times.Once);
    }

    [Fact]
    public async void Update_ScheduledTransactionIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Add(It.IsAny<ScheduledTransaction>()), Times.Never);
    }

    [Fact]
    public async void Update_ScheduledTransactionWithoutAccount_ShouldThrowBusinessException()
    {
        //arrange
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        var scheduledTransaction = _fixture.Build<ScheduledTransaction>()
            .Without(x => x.SourceAccount)
            .With(x => x.Category, category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(scheduledTransaction));

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Add(It.IsAny<ScheduledTransaction>()), Times.Never);
    }

    [Fact]
    public async void Update_ScheduledTransactionWithoutCategory_ShouldThrowBusinessException()
    {
        //arrange
        var scheduledTransaction = _fixture.Build<ScheduledTransaction>()
            .Without(x => x.Category)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(scheduledTransaction));

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Add(It.IsAny<ScheduledTransaction>()), Times.Never);
    }

    [Fact]
    public async void Delete_IdIsValid_ShouldDeleteTransaction()
    {
        //arrange
        var id = _fixture.Create<int>();

        //act
        await _service.Delete(id);

        //assert
        _scheduledTransactionRepoMock.Verify(x => x.Delete(id), Times.Once);
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
        _scheduledTransactionRepoMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
    }
}
