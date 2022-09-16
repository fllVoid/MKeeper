using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(MKeeperDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User> Get(int userId, CancellationToken cancellationToken = default)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        var result = _mapper.Map<User>(userEntity);
        return result;
    }

    public async Task<User> Get(string email, CancellationToken cancellationToken = default)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        var result = _mapper.Map<User>(userEntity);
        return result;
    }

    public async Task<User[]> GetAll(CancellationToken cancellationToken = default)
    {
        var userEntities = await _context.Users
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
        var result = _mapper.Map<User[]>(userEntities);
        return result;
    }

    public async Task<int> Add(User user, CancellationToken cancellationToken = default)
    {
        var userEntity = _mapper.Map<Entities.User>(user);
        await _context.Users.AddAsync(userEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return userEntity.Id;
    }

    public async Task Update(User user, CancellationToken cancellationToken = default)
    {
        var userEntity = _mapper.Map<Entities.User>(user);
        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int userId, CancellationToken cancellationToken = default)
    {
        var userEntity = new Entities.User() { Id = userId };
        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
    }
}
