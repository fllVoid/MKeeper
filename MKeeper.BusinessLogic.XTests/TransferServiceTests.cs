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

public class TransferServiceTests
{
    private readonly Mock<ITransferRepository> _transferRepositoryMock;
    private readonly TransferService _service;
    private readonly Fixture _fixture;

    public TransferServiceTests()
    {
        _transferRepositoryMock = new Mock<ITransferRepository>();
        _service = new TransferService(_transferRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async void Create_TransferIsValid_ShouldCreateNewTransfer()
    {
        //arrange
        var expectedId = _fixture.Create<int>();
        var transfer = _fixture.Create<Transfer>();
        _transferRepositoryMock.Setup(x => x.Add(transfer))
            .ReturnsAsync(expectedId);

        //act
        var actualId = await _service.Create(transfer);

        //assert
        actualId.Should().Be(expectedId);
        _transferRepositoryMock.Verify(x => x.Add(transfer), Times.Once);
    }

    [Fact]
    public async void Create_TransferIsNull_ShouldThrowArgumentNullException()
    {
        //arrange
        Transfer transfer = null;

        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Never);
    }

    [Fact]
    public async void Create_TransferWithoutSourceAccount_ShouldThrowBusinessException()
    {
        //arrange
        var transfer = _fixture.Build<Transfer>()
            .Without(x => x.SourceAccount)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(transfer));

        //assert
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Never);
    }

    [Fact]
    public async void Create_TransferWithoutDestinationAccount_ShouldThrowBusinessException()
    {
        //arrange
        var transfer = _fixture.Build<Transfer>()
            .Without(x => x.DestinationAccount)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(transfer));

        //assert
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Never);
    }

    [Fact]
    public async void Get_ByAccountIds_ShouldReturnTransfers()
    {
        //arrange
        var accountIds = new[] { _fixture.Create<int>() };
        var transfers = new[] { _fixture.Create<Transfer>() };
        _transferRepositoryMock.Setup(x => x.Get(accountIds))
            .ReturnsAsync(transfers);
        //act
        var result = await _service.Get(accountIds);

        //assert
        Assert.Equal(transfers, result);
        _transferRepositoryMock.Verify(x => x.Get(accountIds), Times.Once);
    }

    [Fact]
    public async void Get_ByAccountIdsAndDate_ShouldReturnTransfers()
    {
        //arrange
        var accountIds = new[] { _fixture.Create<int>() };
        var transfers = new[] { _fixture.Create<Transfer>() };
        var from = _fixture.Create<DateTime>();
        var to = from.AddDays(1);
        _transferRepositoryMock.Setup(x => x.Get(accountIds, from, to))
            .ReturnsAsync(transfers);
        //act
        var result = await _service.Get(accountIds, from, to);

        //assert
        Assert.Equal(transfers, result);
        _transferRepositoryMock.Verify(x => x.Get(accountIds, from, to), Times.Once);
    }

    [Fact]
    public async void Update_TransferIsValid_ShouldUpdateTransfer()
    {
        //arrange
        var changedTransfer = _fixture.Create<Transfer>();
        //_transferRepositoryMock.Setup(x => x.Update(changedTransfer))
        //    .Callback(() =>
        //    {
        //        initialTransfer.Sum = changedTransfer.Sum;
        //        initialTransfer.CreationDate = changedTransfer.CreationDate;
        //        initialTransfer.Category = changedTransfer.Category;
        //        initialTransfer.SourceAccount = changedTransfer.SourceAccount;
        //        initialTransfer.Comment = changedTransfer.Comment;
        //    });

        //act
        await _service.Update(changedTransfer);

        //assert
        _transferRepositoryMock.Verify(x => x.Update(changedTransfer), Times.Once);
    }

    [Fact]
    public async void Update_TransferIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Never);
    }

    [Fact]
    public async void Update_TransferWithoutSourceAccount_ShouldThrowBusinessException()
    {
        //arrange
        var transfer = _fixture.Build<Transfer>()
            .Without(x => x.SourceAccount)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(transfer));

        //assert
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Never);
    }

    [Fact]
    public async void Update_TransferWithoutDestinationSource_ShouldThrowBusinessException()
    {
        //arrange
        var transfer = _fixture.Build<Transfer>()
            .Without(x => x.DestinationAccount)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(transfer));

        //assert
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Never);
    }

    [Fact]
    public async void Delete_IdIsValid_ShouldDeleteTransfer()
    {
        //arrange
        var id = _fixture.Create<int>();

        //act
        await _service.Delete(id);

        //assert
        _transferRepositoryMock.Verify(x => x.Delete(id), Times.Once);
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
        _transferRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
    }
}
