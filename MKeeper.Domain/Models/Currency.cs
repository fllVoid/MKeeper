namespace MKeeper.Domain.Models;

public class Currency
{
    public Currency() { }

    public Currency(Currency currency)
    {
        Id = currency.Id;
        AlphabeticCode = currency.AlphabeticCode;
        AtomSymbol = currency.AtomSymbol;
        ExchangeRate = currency.ExchangeRate;
        IsBase = currency.IsBase;
    }

    public int Id { get; set; }
    public string AlphabeticCode { get; set; } = null!;
    public char AtomSymbol { get; set; }
    public decimal ExchangeRate { get; set; }
    public bool IsBase { get; set; }
}
