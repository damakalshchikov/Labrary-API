using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.Sendy.CSharp.TestTask.DataBase;
using Solution.Sendy.CSharp.TestTask.DataBase.Models;
using Solution.Sendy.CSharp.TestTask.DTOs;

namespace Solution.Sendy.CSharp.TestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    // Контекст БД для доступа к ней, маппер для взаимодействия между моделями
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public AuthorController(DatabaseContext db, IMapper mapper)
    {
        _context = db;
        _mapper = mapper;
    }

    // GET список
    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        // Получаем список авторов
        var authors = await _context.Authors.ToListAsync();

        // Если список пустой - 404 код
        if (!authors.Any()) throw new InvalidOperationException("В базе данных отсутствуют авторы");

        // Код 200. Возвращаем DTO-объект клиенту
        return Ok(_mapper.Map<List<AuthorDTO>>(authors));
    }

    // GET по Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorAsync(int id)
    {
        // Получаем конкретного автора по его Id
        var author = await _context.Authors.FindAsync(id);

        // Если нет такой записи - 404 код
        if (author is null) throw new KeyNotFoundException($"Автор с Id={id} не найден");

        // Код 200. Возвращаем DTO-объект клиенту
        return Ok(_mapper.Map<AuthorDTO>(author));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorDTO dto)
    {
        // Создаём автора из переданных данных клиента
        var author = _mapper.Map<Author>(dto);

        // Добавляем созданного автора и сохраняем изменения в БД
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();

        // Код 201. Успешное создание записи
        return CreatedAtRoute(new { id = author.AuthorId }, _mapper.Map<AuthorDTO>(author));
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthorAsunc(int id, [FromBody] UpdateAuthorDTO dto)
    {
        // Получаем автора по Id
        var author = await _context.Authors.FindAsync(id);

        // Если нет такой записи - 404 код
        if (author is null) throw new KeyNotFoundException($"Автор с Id={id} не найден");

        // Преобразуем DTO-объект в Author
        _mapper.Map(dto, author);

        // Сохранение изменений в БД
        await _context.SaveChangesAsync();

        // Код 204. Пустой ответ
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(int id)
    {
        // Получаем автора по Id
        var author = await _context.Authors.FindAsync(id);

        // Если нет такой записи - 404 код
        if (author is null) throw new KeyNotFoundException($"Автор с Id={id} не найден");

        // Удаляем автора и сохраняем изменения в БД
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        // Код 200. Успешное удаление
        return Ok();
    }
}