using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.Sendy.CSharp.TestTask.DataBase;
using Solution.Sendy.CSharp.TestTask.DataBase.Models;
using Solution.Sendy.CSharp.TestTask.DTOs;

namespace Solution.Sendy.CSharp.TestTask.Controllers;

/// <summary>
/// Контроллер для управления книгами
/// </summary>
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

    /// <summary>
    /// Получить список всех книг с информацией об авторах
    /// </summary>
    /// <returns>Список книг</returns>
    /// <response code="200">Возвращает список книг</response>
    /// <response code="404">Если книги не найдены</response>
    /// <response code="401">Если отсутствует API ключ</response>
    /// <response code="403">Если API ключ неверный</response>
    // GET список
    [HttpGet]
    [ProducesResponseType(typeof(List<BookDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetBooksAsync()
    {
        // Получаем список книг
        var books = await _context.Books.ToListAsync();

        // Если список пустой - 404 код
        if (!books.Any()) throw new InvalidOperationException("В базе данных отсутствуют книги");

        // Код 200. Возвращаем список DTO-объектов клиенту
        return Ok(_mapper.Map<List<BookDTO>>(books));
    }

    /// <summary>
    /// Получить книгу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>Информация о книге</returns>
    /// <response code="200">Возвращает информацию о книге</response>
    /// <response code="400">Если идентификатор некорректный</response>
    /// <response code="404">Если книга не найдена</response>
    /// <response code="401">Если отсутствует API ключ</response>
    /// <response code="403">Если API ключ неверный</response>
    // GET по Id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        // Проверяем корректность Id
        if (id <= 0) throw new ArgumentException("Id книги не может быть отрицательным");

        // Получаем конкретную книгу по её Id
        var book = await _context.Books.FindAsync(id);

        // Если такой записи нет - 404 код.
        if (book is null) throw new KeyNotFoundException($"Книга с Id={id} не найдена");

        // Код 200. Возвращаем DTO-объект клиенту
        return Ok(_mapper.Map<BookDTO>(book));
    }

    /// <summary>
    /// Создать новую книгу
    /// </summary>
    /// <param name="dto">Данные для создания книги</param>
    /// <returns>Созданная книга</returns>
    /// <response code="201">Книга успешно создана</response>
    /// <response code="400">Если данные некорректные, автор не существует или книга с таким названием уже существует</response>
    /// <response code="401">Если отсутствует API ключ</response>
    /// <response code="403">Если API ключ неверный</response>
    // POST
    [HttpPost]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PostBookAsync([FromBody] CreateBookDTO dto)
    {
        // Создаём книгу из переданных данных клиента
        var book = _mapper.Map<Book>(dto);

        // Проверяем, нет ли книги с таким же названием. Если есть - 400 код
        var existingBook = _context.Books.FirstOrDefault(b => b.Title == book.Title);
        if (!(existingBook is null)) throw new ArgumentException($"Книга с названием {book.Title} уже существует");

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
        // Проверяем корректность Id
        if (id <= 0) throw new ArgumentException("Id книги не может быть отрицательным");

        // Получаем книгу по Id
        var book = await _context.Books.FindAsync(id);

        // Если нет такой записи - 404 код
        if (book is null) throw new KeyNotFoundException($"Книга с Id={id} не найдена");

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
        // Проверяем корректность Id
        if (id <= 0) throw new ArgumentException("Id книги не может быть отрицательным");

        // Получаем книгу по Id
        var book = await _context.Books.FindAsync(id);

        // Если нет такой записи - 404 код
        if (book is null) throw new KeyNotFoundException($"Книга с Id={id} не найдена");

        // Удаляем книгу и сохраняем изменения в БД
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        // Код 200. Успешное удаление
        return Ok();
    }
}