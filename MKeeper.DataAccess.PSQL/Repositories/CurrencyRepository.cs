using MKeeper.Domain.Models;
using MKeeper.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MKeeper.DataAccess.PSQL.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly MKeeperDbContext _context;
    private readonly IMapper _mapper;

    public CurrencyRepository(MKeeperDbContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(Currency currency)
    {
        var mapped = _mapper.Map<Entities.Currency>(currency);
        var result = await _context.Currencies
            .AddAsync(mapped);
        await _context.SaveChangesAsync();
        return mapped.Id;
    }

    public async Task DeleteAsync(int currencyId)
    {
        _context.Currencies
            .Remove(new Entities.Currency { Id = currencyId });
        await _context.SaveChangesAsync();
    }

    public async Task<Currency[]> GetAllAsync()
    {
        var currencies = await _context.Currencies
            .AsNoTracking()
            .ToArrayAsync();
        return _mapper.Map<Domain.Models.Currency[]>(currencies);
    }

    public async Task<Currency> GetByAlphaCodeAsync(string alphaCode)
    {
        var result = await _context.Currencies
            .AsNoTracking()
            .SingleAsync(entry => entry.AlphaCode == alphaCode);
        return _mapper.Map<Domain.Models.Currency>(result);
    }

    public async Task<Currency> GetByNumericCodeAsync(string numericCode)
    {
        //var result = await _context.Currencies
        //    .AsNoTracking()
        //    .SingleAsync(entry => entry.NumericCode == numericCode);
        //return _mapper.Map<Domain.Models.Currency>(result);
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Currency currency)
    {
        var mapped = _mapper.Map<Entities.Currency>(currency);
        _context.Currencies
            .Update(mapped);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Currency[] currencies)
    {
        var mapped = _mapper.Map<Entities.Currency[]>(currencies);
        _context.Currencies
            .UpdateRange(mapped);
        await _context.SaveChangesAsync();
    }
}
