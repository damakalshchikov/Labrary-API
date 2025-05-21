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
    public IActionResult GetBooks()
    {
        // Получаем все книги, которые есть в БД
        var books = _context.Books.ToList();

        // Код 200. Возвращаем список DTO-объектов клиентов
        return Ok(_mapper.Map<List<BookDTO>>(books));
    }

    // GET по Id
    [HttpGet("{id}")]
    public IActionResult GetBook(int id)
    {
        // Получаем конкретную книгу по её Id
        var book = _context.Books.Find(id);

        // Если такой записи нет - 404 код.
        if (book == null) return NotFound();

        // Код 200. Возвращаем DTO-объект клиенту
        return Ok(_mapper.Map<BookDTO>(book));
    }

    // POST
    [HttpPost]
    public IActionResult PostBook([FromBody] CreateBookDTO dto)
    {
        // Создаём книгу из переданных данных клиента
        var book = _mapper.Map<Book>(dto);

        // Добавляем созданную книгу и сохраняем изменения в БД
        _context.Books.Add(book);
        _context.SaveChanges();

        // Код 201. Успешное создание записи
        return CreatedAtAction(nameof(GetBook), new {id = book.BookId}, null);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult PutBook(int id, [FromBody] UpdateBookDTO dto)
    {
        // Получаем книгу по Id
        var book = _context.Books.Find(id);

        // Если нет такой записи - 404 код
        if (book == null) return NotFound();

        // Преобразуем DTO-объект в Book
        _mapper.Map(dto, book);

        // Сохраняем изменения в БД
        _context.SaveChanges();

        // Код 204. Пустой ответ
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        // Получаем книгу по Id
        var book = _context.Books.Find(id);

        // Если нет такой записи - 404 код
        if (book == null) return NotFound();

        // Удаляем книгу и сохраняем изменения в БД
        _context.Books.Remove(book);
        _context.SaveChanges();

        // Код 200. Успешное удаление
        return Ok();
    }
}