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

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly CategoryService _service;
    private readonly Fixture _fixture;

    public CategoryServiceTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _service = new CategoryService(_categoryRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async void Create_CategoryIsValid_ShouldCreateNewCategory()
    {
        //arrange
        var expectedId = _fixture.Create<int>();
        var category = _fixture.Build<Category>()
            .Without(x => x.ParentCategory)
            .Create();
        _categoryRepositoryMock.Setup(x => x.Add(category))
            .ReturnsAsync(expectedId);

        //act
        var actualId = await _service.Create(category);

        //assert
        actualId.Should().Be(expectedId);
        _categoryRepositoryMock.Verify(x => x.Add(category), Times.Once);
    }

    [Fact]
    public async void Create_CategoryIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));

        //assert
        _categoryRepositoryMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Never);
    }

    [Theory]
    [InlineData(0, "", 0)]
    [InlineData(0, null, 0)]
    [InlineData(-1, "", 0)]
    [InlineData(-1, null, 0)]
    [InlineData(0, "", -1)]
    [InlineData(0, null, -1)]
    [InlineData(-1, "", -1)]
    [InlineData(-1, null, -1)]
    public async void Create_CategoryIsInvalid_ShouldThrowBusinessException(
        int categoryId,
        string categoryName,
        int userId)
    {
        //arrange
        Category category = new Category { Id = categoryId, Name = categoryName, User = new User { Id = userId } };

        //act
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _service.Create(category));

        //assert
        _categoryRepositoryMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Never);
    }

    [Fact]
    public async void Get_ShouldReturnCategories()
    {
        //arrange
        var userId = _fixture.Create<int>();
        var categories = new[] { _fixture.Build<Category>().Without(x => x.ParentCategory).Create() };
        _categoryRepositoryMock.Setup(x => x.Get(userId))
            .ReturnsAsync(categories);
        //act
        var result = await _service.Get(userId);

        //assert
        Assert.Equal(categories, result);
        _categoryRepositoryMock.Verify(x => x.Get(userId), Times.Once);
    }

    [Fact]
    public async void GetSubcategories_ShouldReturnCategories()
    {
        //arrange
        var parentCategoryId = _fixture.Create<int>();
        var categories = new[] { _fixture.Build<Category>().Without(x => x.ParentCategory).Create() };
        _categoryRepositoryMock.Setup(x => x.GetChild(parentCategoryId))
            .ReturnsAsync(categories);
        //act
        var result = await _service.GetSubcategories(parentCategoryId);

        //assert
        Assert.Equal(categories, result);
        _categoryRepositoryMock.Verify(x => x.GetChild(parentCategoryId), Times.Once);
    }

    [Fact]
    public async void Update_CategoryIsValid_ShouldUpdateCategory()
    {
        //arrange
        var initialCategory = _fixture.Build<Category>().Without(x => x.ParentCategory).Create();
        var changedCategory = new Category { 
            Id = initialCategory.Id, 
            Name = _fixture.Create<string>(), 
            IsIncoming = initialCategory.IsIncoming,
            User = initialCategory.User    
        };
        _categoryRepositoryMock.Setup(x => x.Update(changedCategory))
            .Callback(() =>
            {
                initialCategory.Name = changedCategory.Name;
            });

        //act
        await _service.Update(changedCategory);

        //assert
        Assert.Equal(changedCategory.Name, initialCategory.Name);
        _categoryRepositoryMock.Verify(x => x.Update(changedCategory), Times.Once);
    }

    [Fact]
    public async void Update_CategoryIsNull_ShouldThrowArgumentNullException()
    {
        //act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));

        //assert
        _categoryRepositoryMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async void Update_CategoryIsInvalid_ShouldReturnBusinessException(string categoryName)
    {
        //arrange
        var category = _fixture.Build<Category>()
            .With(x => x.Name, categoryName)
            .Without(x => x.ParentCategory)
            .Create();

        //act
        var ex = await Assert.ThrowsAsync<BusinessException>(() => _service.Update(category));

        //assert
        _categoryRepositoryMock.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
    }

    [Fact]
    public async void Delete_IdIsValid_ShouldDeleteCategory()
    {
        //arrange
        var id = _fixture.Create<int>();

        //act
        await _service.Delete(id);

        //assert
        _categoryRepositoryMock.Verify(x => x.Delete(id), Times.Once);
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
        _categoryRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
    }
}
