using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public TransactionRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Add(Transaction transaction, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Transaction>(transaction);
        await _context.Transactions.AddAsync(mapped, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }

    public async Task Delete(int transactionId, CancellationToken cancellationToken)
    {
        var entity = new Entities.Transaction() { Id = transactionId };
        _context.Transactions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var entities = await _context.Transactions
            .AsNoTracking()
            .Where(x => x.CreationDate >= from && x.CreationDate <= to
                && accountIds.Contains(x.SourceAccount.Id) && categoryIds.Contains(x.Category.Id))
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Transaction[]>(entities);
        return mapped;
    }

    public async Task<Transaction[]> Get(int[] accountIds, int[] categoryIds, CancellationToken cancellationToken)
    {
        var entities = await _context.Transactions
            .AsNoTracking()
            .Where(x => accountIds.Contains(x.SourceAccount.Id) && categoryIds.Contains(x.Category.Id))
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Transaction[]>(entities);
        return mapped;
    }

    public async Task<Transaction[]> Get(int[] accountIds, CancellationToken cancellationToken)
    {
        var entities = await _context.Transactions
            .AsNoTracking()
            .Where(x => accountIds.Contains(x.SourceAccount.Id))
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Transaction[]>(entities);
        return mapped;
    }

    public async Task Update(Transaction transaction, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Transaction>(transaction);
        _context.Transactions.Update(mapped);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
