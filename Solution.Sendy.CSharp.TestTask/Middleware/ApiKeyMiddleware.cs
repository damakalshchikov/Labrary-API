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
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Отсутствует API ключ");
            return;
        }

        // Если API ключ неверный - код 403
        if (!_apiKey.Equals(apiKey))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Неверный API ключ");
            return;
        }

        // Если API ключ есть и он верный - пропускаем запрос
        await _next(context);
    }
}