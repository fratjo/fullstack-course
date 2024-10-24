using System.Data.SqlClient;
using Devoir.Repositories.Errors;

namespace Devoir.CustomException;

public static class ExceptionMapper
{
    public static BaseApplicationException MapToApplicationException(this Exception ex)
    {
        if (ex is SqlException sqlEx)
        {
            string message = (SQLServerExceptionCode)sqlEx.Number switch
            {
                SQLServerExceptionCode.UniqueViolation => $"Unique constraint violation. ErrorCode : {sqlEx.Number} InternalMessage : {sqlEx.Message}",
                SQLServerExceptionCode.ForeignKeyViolation => $"Foreign key constraint violation. ErrorCode : {sqlEx.Number} InternalMessage : {sqlEx.Message}",
                SQLServerExceptionCode.SyntaxError or 
                    SQLServerExceptionCode.InvalidColumnName or 
                    SQLServerExceptionCode.InvalidObjectName => $"SQL syntax error. ErrorCode : {sqlEx.Number} InternalMessage : {sqlEx.Message}",
                _ => $"A database error occurred. ErrorCode : {sqlEx.Number} InternalMessage : {sqlEx.Message}"
            };
            return new DatabaseException(message, sqlEx);
        }
        else if (ex is FileNotFoundException)
        {
            return new NotFoundException("The requested file was not found.", ex);
        }
        else if (ex is UnauthorizedAccessException)
        {
            return new BaseApplicationException("Access denied.", 403, ex); // 403: Forbidden
        }
        
        return new BaseApplicationException("An internal error occurred.", 500, ex); // 500: Internal Server Error
    }
}