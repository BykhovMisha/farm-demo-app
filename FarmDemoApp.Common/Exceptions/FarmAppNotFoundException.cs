namespace FarmDemoApp.Common.Exceptions;

public class FarmAppNotFoundException : Exception
{
    public FarmAppNotFoundException(string? message) : base(message)
    {
    }
}
