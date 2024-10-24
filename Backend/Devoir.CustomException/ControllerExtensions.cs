using Microsoft.AspNetCore.Mvc;

namespace Devoir.CustomException;

public static class ControllerExtensions
{
    public static async Task<IActionResult> HandleRequestAsync(this ControllerBase controller, Func<Task<IActionResult>> action, IEnumerable<IExceptionHandler> exceptionHandlers)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            var exceptionHandler = exceptionHandlers.FirstOrDefault(handler => handler.CanHandle(ex));
            return exceptionHandler?.Handle(ex, controller) ?? controller.StatusCode(500, "An unexpected error occurred.");
        }
    }
}