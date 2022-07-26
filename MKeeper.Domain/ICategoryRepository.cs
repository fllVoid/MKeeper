using MKeeper.Domain.Models;

namespace MKeeper.Domain;

public interface ICategoryRepository
{
    Category GetCategory(int categoryId);
    IEnumerable<Category> GetCategories(int userId);
    IEnumerable<Category> GetChildCategories(int parentCategoryId);
    IEnumerable<Category> GetIncomeCategories(int userId);
    IEnumerable<Category> GetExpenseCategories(int userId);
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int categoryId);
}
