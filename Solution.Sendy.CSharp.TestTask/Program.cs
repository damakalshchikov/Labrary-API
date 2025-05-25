using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Solution.Sendy.CSharp.TestTask.DataBase;
using Solution.Sendy.CSharp.TestTask.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .WriteTo.Console(
        outputTemplate: builder.Configuration.GetSection("Logging:Console")["ConsoleOutputTemplate"]!
    )
    .WriteTo.File(
        $"./logs/{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.log",
        outputTemplate: builder.Configuration.GetSection("Logging:File")["FileOutputTemplate"]!
    )
    .CreateLogger();

// Подключаем Serilog
builder.Host.UseSerilog();

// Подключаем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Основная информация об API
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Library API",
        Description = "Web API для управления авторами и книгами.",
        Contact = new OpenApiContact
        {
            Name = "damakalshchikov",
            Url = new Uri("https://github.com/damakalshchikov"),
        }
    });

    // Настройка для API ключа
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "x-api-key",
        Description = "Введите ваш API ключ в поле ниже"
    });

    // Требование API ключа для всех операций
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] { }
        }
    });
});

// Регистрируем DatabaseContext
builder.Services.AddDbContext<DatabaseContext>(options
    => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Добавляем контроллеры
builder.Services.AddControllers();

// Собираем приложение
var app = builder.Build();

// Создаем экземпляр ILogger
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Сборка приложения завершена");

if (app.Environment.IsDevelopment())
{
    // Включаем Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Library API";
        options.SwaggerEndpoint("/v1/swagger.json", "Library API");
        options.DisplayRequestDuration();
    });
}

// Добавляем middleware для логирования времени запросов
app.UseMiddleware<TimingMiddleware>();

// Добавляем middleware для обработки исключений
app.UseMiddleware<ExceptionsHandlerMiddleware>();

// Добавляем middleware для проверки API ключа
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

// Добавляем маршрутизацию для контроллеров
app.MapControllers();

logger.LogInformation($"Приложение успешно запущено по адресу: {builder.Configuration["ASPNETCORE_URLS"]}");
logger.LogInformation("SwaggerUI доступен по адресу: /swagger");
app.Run();
logger.LogInformation("Приложение успешно остановлено");
