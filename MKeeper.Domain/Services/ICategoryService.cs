using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ICategoryService
{
    Task<int> Create(Category category);

    Task<Category[]> Get(int userId);

    Task<Category[]> GetSubcategories(int parentCategoryId);

    Task Update(Category category);

    Task Delete(int categoryId);
}
