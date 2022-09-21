using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category[]> Get(int userId, CancellationToken cancellationToken = default);

    Task<Category[]> GetChild(int parentCategoryId, CancellationToken cancellationToken = default);

    Task<Category[]> GetIncome(int userId, CancellationToken cancellationToken = default);

    Task<Category[]> GetExpense(int userId, CancellationToken cancellationToken = default);

    Task<int> Add(Category category, CancellationToken cancellationToken = default);

    Task Update(Category category, CancellationToken cancellationToken = default);

    Task Delete(int categoryId, CancellationToken cancellationToken = default);
}
