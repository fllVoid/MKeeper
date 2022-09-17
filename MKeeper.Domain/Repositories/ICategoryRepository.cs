using MKeeper.Domain.Models;

namespace MKeeper.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category[]> Get(int userId, CancellationToken cancellationToken);

    Task<Category[]> GetChild(int parentCategoryId, CancellationToken cancellationToken);

    Task<Category[]> GetIncome(int userId, CancellationToken cancellationToken);

    Task<Category[]> GetExpense(int userId, CancellationToken cancellationToken);

    Task<int> Add(Category category, CancellationToken cancellationToken);

    Task Update(Category category, CancellationToken cancellationToken);

    Task Delete(int categoryId, CancellationToken cancellationToken);
}
