using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public AccountRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Add(Account account, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Account>(account);
        await _context.Accounts.AddAsync(mapped, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }

    public async Task Delete(int accountId, CancellationToken cancellationToken)
    {
        var entity = new Entities.Account() { Id = accountId };
        _context.Accounts.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Account[]> Get(int userId, CancellationToken cancellationToken)
    {
        var entities = await _context.Accounts
            .AsNoTracking()
            .Where(x => x.User.Id == userId)
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Account[]>(entities);
        return mapped;
    }

    public async Task<Account[]> GetAll(CancellationToken cancellationToken)
    {
        var entities = await _context.Accounts
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
        var mapped = _mapper.Map<Account[]>(entities);
        return mapped;
    }

    public async Task Update(Account account, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Entities.Account>(account);
        _context.Accounts.Update(mapped);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
