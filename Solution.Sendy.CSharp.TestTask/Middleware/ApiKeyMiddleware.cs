namespace Solution.Sendy.CSharp.TestTask.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration["ApiSettings:ApiKey"]!;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Если отсутствует API ключ - код 401
        if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey))
        {
            throw new UnauthorizedAccessException("Отсутствует API ключ");
        }

        // Если API ключ неверный - код 403
        if (!_apiKey.Equals(apiKey))
        {
            throw new UnauthorizedAccessException("Неверный API ключ");
        }

        // Если API ключ есть и он верный - пропускаем запрос
        await _next(context);
    }
}