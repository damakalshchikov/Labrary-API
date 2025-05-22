using System.Diagnostics;

namespace Solution.Sendy.CSharp.TestTask.Middleware;

public class TimingMiddleware
{
    // Следующий делегат в цепочке запросов
    private readonly RequestDelegate _next;
    // Логгер для записи информации о запросах
    private readonly ILogger<TimingMiddleware> _logger;

    public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Метод InvokeAsync обрабатывает запросы
    public async Task InvokeAsync(HttpContext context)
    {
        // Создаем Stopwatch для измерения времени выполнения запроса
        var stopwatch = new Stopwatch();

        try
        {
            // Запускаем Stopwatch
            stopwatch.Start();
            // Передаем запрос следующему middleware в цепочке
            await _next(context);
        }
        finally
        {
            // Останавливаем Stopwatch
            stopwatch.Stop();
            // Получаем время выполнения запроса в миллисекундах
            var elapsed = stopwatch.Elapsed.TotalMilliseconds;

            // Записываем информацию о запросе в лог
            _logger.LogInformation(
                "Запрос {Method} {Path} завершен за {Elapsed}мс. Статус: {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                elapsed,
                context.Response.StatusCode
            );
        }
    }    
}
