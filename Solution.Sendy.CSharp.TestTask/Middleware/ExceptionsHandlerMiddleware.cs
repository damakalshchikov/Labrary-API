using Solution.Sendy.CSharp.TestTask.Exceptions;

namespace Solution.Sendy.CSharp.TestTask.Middleware;

public class ExceptionsHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsHandlerMiddleware> _logger;

    public ExceptionsHandlerMiddleware(RequestDelegate next, ILogger<ExceptionsHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (ex is ArgumentNullException)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
        }

        catch (Exception ex) when (ex is InvalidOperationException || ex is KeyNotFoundException)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = new ExceptionsResponse(
            exception.Message,
            exception.GetType().Name,
            StatusCodes.Status500InternalServerError
        );

        await context.Response.WriteAsJsonAsync(exceptionResponse);
    }
}