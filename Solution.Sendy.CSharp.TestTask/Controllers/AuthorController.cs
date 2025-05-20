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
        var authors = _mapper.Map<List<AuthorDTO>>(_context.Authors.ToList());

        return Ok(authors);
    }

    // GET по Id
    [HttpGet("{id}")]
    public IActionResult GetAuthor(int id)
    {
        // Получаем конкретного автора по его Id
        var author = _mapper.Map<AuthorDTO>(_context.Authors.Find(id));

        return Ok(author);
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
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorDTO dto)
    {
        // Получаем автора по Id
        var author = _mapper.Map<Author>(_context.Authors.Find(id));

        // Если новые данные - NULL, то не перезаписываем их
        author.FirstName = dto.FirstName ?? author.FirstName;
        author.LastName = dto.LastName ?? author.LastName;
        author.Email = dto.Email ?? author.Email;

        // Обновляем автора и сохраняем изменения в БД
        _context.Authors.Update(author);
        _context.SaveChanges();

        return Ok();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        // Получаем автора по Id
        var author = _mapper.Map<Author>(_context.Authors.Find(id));

        // Удаляем автора и сохраняем изменения в БД
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return Ok();
    }
}