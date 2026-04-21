namespace BlogApp.Services.Exceptions;

public class ValidationException(string message) : ServiceException(message);

