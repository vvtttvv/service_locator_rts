namespace ServiceLocator.Exceptions;

public class DifferentOriginException : Exception
{
    public DifferentOriginException()
        : base("Interface and implementation have different origins.") { }

    public DifferentOriginException(string message)
        : base(message) { }

    public DifferentOriginException(string message, Exception innerException)
        : base(message, innerException) { }
}