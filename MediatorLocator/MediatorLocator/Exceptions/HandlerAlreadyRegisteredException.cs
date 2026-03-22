namespace MediatorLocator.Exceptions;

public class HandlerAlreadyRegisteredException : Exception
{
    public HandlerAlreadyRegisteredException()
        : base("Handler already was registered!") { }

    public HandlerAlreadyRegisteredException(string message)
        : base(message) { }

    public HandlerAlreadyRegisteredException(string message, Exception innerException)
        : base(message, innerException) { }
}