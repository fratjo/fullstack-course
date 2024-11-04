using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Devoir.CustomException;

public interface IExceptionHandler
{
    bool CanHandle(Exception ex);
    IActionResult Handle(Exception ex, ControllerBase controller);
}

public static class AddExceptionHandlerExtensions
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, ValidationExceptionHandler>();
        services.AddSingleton<IExceptionHandler, NotFoundExceptionHandler>();
        services.AddSingleton<IExceptionHandler, BaseApplicationExceptionHandler>();
        services.AddSingleton<IExceptionHandler, GeneralExceptionHandler>();
    }
}

public class ValidationExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is ValidationException;
    }

    public IActionResult Handle(Exception exception, ControllerBase controller)
    {
        var validationException = exception as ValidationException;
        return controller.BadRequest(new { message = validationException!.Message });
    }
}


public class NotFoundExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is NotFoundException;
    }

    public IActionResult Handle(Exception exception, ControllerBase controller)
    {
        var notFoundException = exception as NotFoundException;
        return controller.NotFound(new { message = notFoundException!.Message });
    }
}

public class BaseApplicationExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return exception is BaseApplicationException; // Gère toutes les exceptions de base
    }

    public IActionResult Handle(Exception exception, ControllerBase controller)
    {
        var baseException = exception as BaseApplicationException;
        return controller.StatusCode(500, "An internal error occurred.");
    }
}

public class GeneralExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception exception)
    {
        return true; // Gère toutes les autres exceptions
    }

    public IActionResult Handle(Exception exception, ControllerBase controller)
    {
        return controller.StatusCode(500, "An unexpected error occurred.");
    }
}