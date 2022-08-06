using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MKeeperDbContext _context;

    public CategoryRepository(MKeeperDbContext context) => _context = context;

    public async Task<int> Add(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Category[]> Get(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Category[]> GetChild(int parentCategoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Category[]> GetExpense(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Category[]> GetIncome(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Category category)
    {
        throw new NotImplementedException();
    }
}
