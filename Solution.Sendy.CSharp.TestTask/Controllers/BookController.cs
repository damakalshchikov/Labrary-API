using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Solution.Sendy.CSharp.TestTask.DataBase;
using Solution.Sendy.CSharp.TestTask.DataBase.Models;
using Solution.Sendy.CSharp.TestTask.DTOs;

namespace Solution.Sendy.CSharp.TestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    // Контекст БД для доступа к ней, маппер для взаимодействия между моделями
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public BookController(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET список
    [HttpGet]
    public async Task<IActionResult> GetBooksAsync()
    {
        // Получаем список книг
        var books = await _context.Books.ToListAsync();

        // Код 200. Возвращаем список DTO-объектов клиентов
        // Код 200. Возвращаем список DTO-объектов клиенту
        return Ok(_mapper.Map<List<BookDTO>>(books));
    }

    // GET по Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        // Получаем конкретную книгу по её Id
        var book = await _context.Books.FindAsync(id);

        // Если такой записи нет - 404 код.
        if (book == null) return NotFound();

        // Код 200. Возвращаем DTO-объект клиенту
        return Ok(_mapper.Map<BookDTO>(book));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> PostBookAsync([FromBody] CreateBookDTO dto)
    {
        // Создаём книгу из переданных данных клиента
        var book = _mapper.Map<Book>(dto);

        // Добавляем созданную книгу и сохраняем изменения в БД
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        // Код 201. Успешное создание записи
        return CreatedAtRoute(new {id = book.BookId}, null);
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBookAsync(int id, [FromBody] UpdateBookDTO dto)
    {
        // Получаем книгу по Id
        var book = await _context.Books.FindAsync(id);

        // Если нет такой записи - 404 код
        if (book == null) return NotFound();

        // Преобразуем DTO-объект в Book
        _mapper.Map(dto, book);

        // Сохраняем изменения в БД
        await _context.SaveChangesAsync();

        // Код 204. Пустой ответ
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        // Получаем книгу по Id
        var book = await _context.Books.FindAsync(id);

        // Если нет такой записи - 404 код
        if (book == null) return NotFound();

        // Удаляем книгу и сохраняем изменения в БД
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        // Код 200. Успешное удаление
        return Ok();
    }
}