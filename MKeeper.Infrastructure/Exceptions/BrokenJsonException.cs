namespace MKeeper.Infrastructure.Exceptions;

public class BrokenJsonException : Exception
{
    public BrokenJsonException(string message) : base(message)
    { }
    public BrokenJsonException(string message, Exception innerException) : base(message, innerException)
    { }
}
