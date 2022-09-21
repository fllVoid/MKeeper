using MKeeper.Domain.Models;
using MKeeper.Domain.Common.CustomResults;

namespace MKeeper.Domain.Services;

public interface ICategoryService
{
    Task<Result<int>> Create(Category category);

    Task<Result<Category[]>> Get(int userId);

    Task<Result<Category[]>> GetSubcategories(int parentCategoryId);

    Task<Result> Update(Category category);

    Task<Result> Delete(int categoryId);
}
