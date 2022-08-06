using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ICategoryService
{
    Task<int> Create(Category category);

    Task Update(Category category);

    Task Delete(int categoryId);
}
