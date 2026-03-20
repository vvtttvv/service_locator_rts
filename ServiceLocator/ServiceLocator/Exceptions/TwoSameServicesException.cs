namespace ServiceLocator.Exceptions;

public class TwoSameServicesException: Exception
{
    public TwoSameServicesException()
        : base("Two types are assigned to the same type of interface.") { }

    public TwoSameServicesException(string message)
        : base(message) { }

    public TwoSameServicesException(string message, Exception innerException)
        : base(message, innerException) { }
}