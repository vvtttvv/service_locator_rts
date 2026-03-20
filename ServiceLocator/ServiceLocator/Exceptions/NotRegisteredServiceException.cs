namespace ServiceLocator.Exceptions;

public class NotRegisteredServiceException: Exception
{
    public NotRegisteredServiceException()
        : base("Type hasn't been registered yet.") { }

    public NotRegisteredServiceException(string message)
        : base(message) { }

    public NotRegisteredServiceException(string message, Exception innerException)
        : base(message, innerException) { }
}