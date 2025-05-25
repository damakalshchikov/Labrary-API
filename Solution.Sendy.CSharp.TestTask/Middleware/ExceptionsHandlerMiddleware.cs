using Microsoft.AspNetCore.Mvc;

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
        // Пытаемся передать запрос следующему middleware в цепочке
        try
        {
            await _next(context);
        }
        // Обрабатываем исключения, которые возникают при передаче некорректных данных
        catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentException)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
        }
        // Обрабатываем исключения, которые возникают при отсутствии API ключа или неверном API ключе
        catch (Exception ex) when (ex is UnauthorizedAccessException)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(
                context,
                ex,
                ex.Message.Contains("Отсутствует") ? StatusCodes.Status401Unauthorized : StatusCodes.Status403Forbidden
            );
        }
        // Обрабатываем исключения, которые возникают при отсутствии данных в БД
        catch (Exception ex) when (ex is InvalidOperationException || ex is KeyNotFoundException)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int status)
    {
        // Подготовка ответа
        var problem = new ProblemDetails
        {
            Type = $"http://localhost:5067/errors/{exception.GetType().Name}",
            Title = exception.GetType().Name,
            Status = status,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        // Отправка ответа
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);
    }
}