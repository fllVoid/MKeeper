namespace MKeeper.Domain.Models;

public class Currency
{
    public Currency(Currency currency)
    {
        Id = currency.Id;
        AlphaCode = currency.AlphaCode;
        AtomSymbol = currency.AtomSymbol;
        ExchangeRate = currency.ExchangeRate;
        IsBase = currency.IsBase;
    }

    public int Id { get; set; }
    public string AlphaCode { get; set; } = null!;
    public char AtomSymbol { get; set; }
    public decimal ExchangeRate { get; set; }
    public bool IsBase { get; set; }
}
