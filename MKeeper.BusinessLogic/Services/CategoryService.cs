using MKeeper.Domain.Exceptions;
using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using MKeeper.Domain.Services;

namespace MKeeper.BusinessLogic.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Create(Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        var isInvalid = string.IsNullOrWhiteSpace(category.Name) || category.User.Id <= 0;
        if (isInvalid)
        {
            throw new BusinessException(nameof(category));
        }
        return await _categoryRepository.Add(category);
    }

    public async Task<Category[]> Get(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException(nameof(userId));
        }
        var categories = await _categoryRepository.Get(userId);
        return categories;
    }

    public async Task<Category[]> GetSubcategories(int parentCategoryId)
    {
        if (parentCategoryId <= 0)
        {
            throw new ArgumentException(nameof(parentCategoryId));
        }
        var subcategories = await _categoryRepository.GetChild(parentCategoryId);
        return subcategories;
    }

    public async Task Update(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        var isInvalid = string.IsNullOrWhiteSpace(category.Name) || category.User.Id <= 0
            || category.Id <= 0;
        if (isInvalid)
        {
            throw new BusinessException(nameof(category));
        }
        await _categoryRepository.Update(category);
    }

    public async Task Delete(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException(nameof(categoryId));
        }
        await _categoryRepository.Delete(categoryId);
    }
}
