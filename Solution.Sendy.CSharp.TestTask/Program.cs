using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Solution.Sendy.CSharp.TestTask.DataBase;
using Solution.Sendy.CSharp.TestTask.Middleware;

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

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Регистрируем DatabaseContext
builder.Services.AddDbContext<DatabaseContext>(options
    => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Добавляем контроллеры
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

Log.Information("Сборка приложения...");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Добавляем middleware для логирования времени запросов
app.UseMiddleware<TimingMiddleware>();

// Добавляем middleware для проверки API ключа
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

// Добавляем маршрутизацию для контроллеров
app.MapControllers();

Log.Information("Приложение успешно запущено");
app.Run();
Log.Information("Приложение успешно остановлено");