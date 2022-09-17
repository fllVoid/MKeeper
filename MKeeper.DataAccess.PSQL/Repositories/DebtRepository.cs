using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class DebtRepository : IDebtRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public DebtRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Add(Debt debt, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Debt>(debt);
        await _context.Debts.AddAsync(mapped, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }

    public async Task Delete(int debtId, CancellationToken cancellationToken)
    {
        var entity = new Entities.Debt() { Id = debtId };
        _context.Debts.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Debt[]> Get(int accountId, CancellationToken cancellationToken)
    {
        var entities = await _context.Debts
            .AsNoTracking()
            .Where(x => x.SourceAccount.Id == accountId)
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Debt[]>(entities);
        return mapped;
    }

    public async Task<Debt[]> GetAll(CancellationToken cancellationToken)
    {
        var entities = await _context.Debts
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Debt[]>(entities);
        return mapped;
    }

    public async Task Update(Debt debt, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Debt>(debt);
        _context.Debts.Update(mapped);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
