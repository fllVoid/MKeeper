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

public class DebtServiceTests
{
    private readonly Mock<IDebtRepository> _debtRepositoryMock;
    private readonly DebtService _service;
    private readonly Fixture _fixture;

    public DebtServiceTests()
    {
        _debtRepositoryMock = new Mock<IDebtRepository>();
        _service = new DebtService(_debtRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async void Create_DebtIsValid_ShouldCreateNewDebt()
    {
        //arrange
        var expectedId = _fixture.Create<int>();
        var debt = _fixture.Create<Debt>();
        _debtRepositoryMock.Setup(x => x.Add(debt))
            .ReturnsAsync(expectedId);
        debt.RepaymentDate = debt.CreationDate.AddDays(1);

        //act
        var actualId = await _service.Create(debt);

        //assert
        actualId.Should().Be(expectedId);
        _debtRepositoryMock.Verify(x => x.Add(debt), Times.Once);
    }

    [Fact]
    public async void Create_DebtIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _debtRepositoryMock.Verify(x => x.Add(It.IsAny<Debt>()), Times.Never);
    }

    [Fact]
    public async void Create_DebtWithoutSourceAccount_ShouldThrowBusinessException()
    {
        //arrange
        var debt = _fixture.Build<Debt>()
            .Without(x => x.SourceAccount)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(debt));

        //assert
        _debtRepositoryMock.Verify(x => x.Add(It.IsAny<Debt>()), Times.Never);
    }

    [Theory]
    [InlineData("01.01.2000", "29.12.1999", "noname")]
    [InlineData("01.01.2000", "01.02.2000", "")]
    [InlineData("01.01.2000", "01.02.2000", null)]
    public async void Create_DebtIsInvalid_ShouldThrowBusinessException(
        string creationDate,
        string repaymentDate,
        string subjectName)
    {
        //arrange
        var debt = _fixture.Build<Debt>()
            .With(x => x.CreationDate, DateTime.Parse(creationDate))
            .With(x => x.RepaymentDate, DateTime.Parse(repaymentDate))
            .With(x => x.SubjectName, subjectName)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(debt));

        //assert
        _debtRepositoryMock.Verify(x => x.Add(It.IsAny<Debt>()), Times.Never);
    }

    [Fact]
    public async void Get_ShouldReturnDebts()
    {
        //arrange
        var accountId = _fixture.Create<int>();
        var debts = new[] { _fixture.Create<Debt>() };
        _debtRepositoryMock.Setup(x => x.Get(accountId))
            .ReturnsAsync(debts);
        //act
        var result = await _service.Get(accountId);

        //assert
        Assert.Equal(debts, result);
        _debtRepositoryMock.Verify(x => x.Get(accountId), Times.Once);
    }

    [Fact]
    public async void Update_DebtIsValid_ShouldUpdateDebt()
    {
        //arrange
        var changedDebt = _fixture.Create<Debt>();
        changedDebt.RepaymentDate = changedDebt.CreationDate.AddDays(1);

        //act
        await _service.Update(changedDebt);

        //assert
        _debtRepositoryMock.Verify(x => x.Update(changedDebt), Times.Once);
    }

    [Fact]
    public async void UpdateIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));

        //assert
        _debtRepositoryMock.Verify(x => x.Add(It.IsAny<Debt>()), Times.Never);
    }

    [Fact]
    public async void Update_DebtWithoutSourceAccount_ShouldThrowBusinessException()
    {
        //arrange
        var debt = _fixture.Build<Debt>()
            .Without(x => x.SourceAccount)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(debt));

        //assert
        _debtRepositoryMock.Verify(x => x.Add(It.IsAny<Debt>()), Times.Never);
    }


    [Theory]
    [InlineData("01.01.2000", "29.12.1999", "noname")]
    [InlineData("01.01.2000", "01.02.2000", "")]
    [InlineData("01.01.2000", "01.02.2000", null)]
    public async void Update_DebtIsInvalid_ShouldThrowBusinessException(
        string creationDate,
        string repaymentDate,
        string subjectName)
    {
        //arrange
        var debt = _fixture.Build<Debt>()
            .With(x => x.CreationDate, DateTime.Parse(creationDate))
            .With(x => x.RepaymentDate, DateTime.Parse(repaymentDate))
            .With(x => x.SubjectName, subjectName)
            .Create();

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(debt));

        //assert
        _debtRepositoryMock.Verify(x => x.Update(It.IsAny<Debt>()), Times.Never);
    }

    [Fact]
    public async void Delete_IdIsValid_ShouldDeleteDebt()
    {
        //arrange
        var id = _fixture.Create<int>();

        //act
        await _service.Delete(id);

        //assert
        _debtRepositoryMock.Verify(x => x.Delete(id), Times.Once);
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
        _debtRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
    }
}
