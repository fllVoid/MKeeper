using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category[]> GetCategories(int userId);

    Task<Category[]> GetChildCategories(int parentCategoryId);

    Task<Category[]> GetIncomeCategories(int userId);

    Task<Category[]> GetExpenseCategories(int userId);

    Task<int> AddCategory(Category category);

    Task UpdateCategory(Category category);

    Task DeleteCategory(int categoryId);
}
