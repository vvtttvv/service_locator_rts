namespace BlogApp.Services.Exceptions;

public abstract class ServiceException(string message) : Exception(message);

