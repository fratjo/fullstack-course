using System.Net;

namespace Devoir.CustomException;

public class BaseApplicationException : Exception
{
    public int StatusCode { get; private set; }

    public BaseApplicationException(string message, int statusCode) 
        : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseApplicationException(string message, int statusCode, Exception innerException) 
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}