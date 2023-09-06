namespace FarmDemoApp.Common.Exceptions;

public class FarmAppException : Exception
{
    public FarmAppException(string? message) : base(message)
    {
    }
}