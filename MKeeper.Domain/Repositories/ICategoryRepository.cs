using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category[]> Get(int userId);

    Task<Category[]> GetChild(int parentCategoryId);

    Task<Category[]> GetIncome(int userId);

    Task<Category[]> GetExpense(int userId);

    Task<int> Add(Category category);

    Task Update(Category category);

    Task Delete(int categoryId);
}
