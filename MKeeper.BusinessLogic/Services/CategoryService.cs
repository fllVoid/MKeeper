using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;
using MKeeper.Domain.Common.CustomResults;
using MKeeper.BusinessLogic.CustomResults;

namespace MKeeper.BusinessLogic.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<int>> Create(Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        var isInvalid = string.IsNullOrWhiteSpace(category.Name) || category.User.Id <= 0;
        if (isInvalid)
        {
            return new InvalidCategoryResult<int>($"Invalid category object:\n{category}");
        }
        var id = await _categoryRepository.Add(category);
        return new SuccessResult<int>(id);
    }

    public async Task<Result<Category[]>> Get(int userId)
    {
        if (userId <= 0)
        {
            return new InvalidIdResult<Category[]>($"Invalid userId value: {userId}");
        }
        var categories = await _categoryRepository.Get(userId);
        return new SuccessResult<Category[]>(categories);
    }

    public async Task<Result<Category[]>> GetSubcategories(int parentCategoryId)
    {
        if (parentCategoryId <= 0)
        {
            return new InvalidIdResult<Category[]>($"Invalid parentCategoryId: {parentCategoryId}");
        }
        var subcategories = await _categoryRepository.GetChild(parentCategoryId);
        return new SuccessResult<Category[]>(subcategories);
    }

    public async Task<Result> Update(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        var isInvalid = string.IsNullOrWhiteSpace(category.Name) || category.User.Id <= 0
            || category.Id <= 0;
        if (isInvalid)
        {
            return new InvalidCategoryResult($"Invalid category object:\n{category}");
        }
        await _categoryRepository.Update(category);
        return new SuccessResult();
    }

    public async Task<Result> Delete(int categoryId)
    {
        if (categoryId <= 0)
        {
            return new InvalidIdResult($"Invalid categoryId value: {categoryId}");
        }
        await _categoryRepository.Delete(categoryId);
        return new SuccessResult();
    }
}
