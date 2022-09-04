using MKeeper.Domain.Models;

namespace MKeeper.Domain.Services;

public interface ICurrencyService
{
    Task<Currency> Get(int id);

    Task<Currency> Get(string alphaCode);

    Task<Currency[]> GetAll();

    //Task Update (Currency currency);

    //Task Update (Currency[] currencies);
}
