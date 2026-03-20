namespace ServiceLocator.Exceptions;

public class MultipleConstructorsException : Exception
{
    public MultipleConstructorsException()
        : base("Type has multiple constructors and cannot be resolved automatically.") { }

    public MultipleConstructorsException(string message)
        : base(message) { }

    public MultipleConstructorsException(string message, Exception innerException)
        : base(message, innerException) { }
}