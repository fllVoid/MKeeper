namespace MKeeper.Domain.Models;

public class Currency
{
    public int Id { get; set; }
    public string AlphaCode { get; set; } = null!;
    public char AtomSymbol { get; set; }
    public decimal ExchangeRate { get; set; }
    public bool IsBase { get; set; }
}
