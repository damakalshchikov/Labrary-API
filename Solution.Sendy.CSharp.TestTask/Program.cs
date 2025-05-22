using Microsoft.EntityFrameworkCore;
using Solution.Sendy.CSharp.TestTask.DataBase;
using Solution.Sendy.CSharp.TestTask.Middleware;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Добавляем middleware для логирования времени запросов
app.UseMiddleware<TimingMiddleware>();

app.UseHttpsRedirection();

// Добавляем маршрутизацию для контроллеров
app.MapControllers();

app.Run();