using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllAuthors()
    {
        // Получаем всех авторов, которые есть в БД
        var authors = _context.Authors.ToList();

        // Код 200. Возвращаем список DTO-объектов клиенту
        return Ok(_mapper.Map<List<AuthorDTO>>(authors));
    }

    // GET по Id
    [HttpGet("{id}")]
    public IActionResult GetAuthor(int id)
    {
        // Получаем конкретного автора по его Id
        var author = _context.Authors.Find(id);

        return Ok(author);
        // Код 200. Возвращаем DTO-объект клиенту
        return Ok(_mapper.Map<AuthorDTO>(author));
    }

    // POST
    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorDTO dto)
    {
        // Создаём автора
        // Если при POST-методе переданы пустые данные - NULL во всех полях,
        // кроме Id
        var author = _mapper.Map<Author>(dto);

        // Добавляем созданного автора и сохраняем изменения в БД
        _context.Authors.Add(author);
        _context.SaveChanges();

        return Ok();
        // Код 201. Успешное создание записи
        return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, null);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorDTO dto)
    {
        // Получаем автора по Id
        var author = _context.Authors.Find(id);

        // Если новые данные - NULL, то не перезаписываем их
        author.FirstName = dto.FirstName ?? author.FirstName;
        author.LastName = dto.LastName ?? author.LastName;
        author.Email = dto.Email ?? author.Email;

        // Преобразуем DTO-объект в Author
        _mapper.Map(dto, author);
        _context.SaveChanges();

        // Код 204. Пустой ответ
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        // Получаем автора по Id
        var author = _context.Authors.Find(id);

        // Если нет такой записи - 404 код
        if (author == null) return NotFound();

        // Удаляем автора и сохраняем изменения в БД
        _context.Authors.Remove(author);
        _context.SaveChanges();

        // Код 200. Успешное удаление
        return Ok();
    }
}