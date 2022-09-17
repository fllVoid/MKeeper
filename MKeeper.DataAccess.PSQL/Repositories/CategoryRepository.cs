using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Add(Category category, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Category>(category);
        await _context.Categories.AddAsync(mapped, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }

    public async Task Delete(int categoryId, CancellationToken cancellationToken)
    {
        var entity = new Entities.Category() { Id = categoryId };
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Category[]> Get(int userId, CancellationToken cancellationToken)
    {
        var entities = await _context.Categories
            .AsNoTracking()
            .Where(x => x.User.Id == userId)
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Category[]>(entities);
        return mapped;
    }

    public async Task<Category[]> GetChild(int parentCategoryId, CancellationToken cancellationToken)
    {
        //TODO maybe trouble here because recursive relation
        var entities = await _context.Categories
            .AsNoTracking()
            .Where(x => x.ParentCategory != null && x.ParentCategory.Id == parentCategoryId)
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Category[]>(entities);
        return mapped;
    }

    public async Task<Category[]> GetExpense(int userId, CancellationToken cancellationToken)
    {
        var entities = await _context.Categories
               .AsNoTracking()
               .Where(x => !x.IsIncoming)
               .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Category[]>(entities);
        return mapped;
    }

    public async Task<Category[]> GetIncome(int userId, CancellationToken cancellationToken)
    {
        var entities = await _context.Categories
               .AsNoTracking()
               .Where(x => x.IsIncoming)
               .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Category[]>(entities);
        return mapped;
    }

    public async Task Update(Category category, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Category>(category);
        _context.Categories.Update(mapped);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
