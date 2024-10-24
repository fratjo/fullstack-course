namespace Devoir.CustomException;

public class NotFoundException : BaseApplicationException
{
    public NotFoundException(string message) 
        : base(message, 404) // 404: Not Found
    {
    }

    public NotFoundException(string message, Exception innerException) 
        : base(message, 404, innerException)
    {
    }
}

public class ValidationException : BaseApplicationException
{
    public ValidationException(string message) 
        : base(message, 400) // 400: Bad Request (validation errors)
    {
    }

    public ValidationException(string message, Exception innerException) 
        : base(message, 400, innerException)
    {
    }
}

public class DatabaseException : BaseApplicationException
{
    public DatabaseException(string message) 
        : base(message, 500) // 500: Internal Server Error (database)
    {
    }
    
    public DatabaseException(string message, Exception innerException) 
        : base(message, 500, innerException)
    {
    }
}