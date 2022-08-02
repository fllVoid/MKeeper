using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task<Category[]> GetCategories(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Category[]> GetChildCategories(int parentCategoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Category[]> GetExpenseCategories(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Category[]> GetIncomeCategories(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(int categoryId)
    {
        throw new NotImplementedException();
    }
}
