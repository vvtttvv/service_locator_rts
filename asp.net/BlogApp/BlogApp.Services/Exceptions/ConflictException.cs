namespace BlogApp.Services.Exceptions;

public class ConflictException(string message) : ServiceException(message);

