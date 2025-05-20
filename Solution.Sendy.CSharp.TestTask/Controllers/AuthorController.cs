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
        var authors = _mapper.Map<List<AuthorDTO>>(_context.Authors.ToList());

        return Ok(authors);
    }

    // GET по Id
    [HttpGet("{id}")]
    public IActionResult GetAuthor(int id)
    {
        var author = _mapper.Map<AuthorDTO>(_context.Authors.Find(id));

        return Ok(author);
    }

    // POST
    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorDTO dto)
    {
        var author = _mapper.Map<Author>(dto);

        _context.Authors.Add(author);
        _context.SaveChanges();

        return Ok();
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorDTO dto)
    {
        var author = _mapper.Map<Author>(_context.Authors.Find(id));
        author.FirstName = dto.FirstName ?? author.FirstName;
        author.LastName = dto.LastName ?? author.LastName;
        author.Email = dto.Email ?? author.Email;

        _context.Authors.Update(author);
        _context.SaveChanges();

        return Ok();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var author = _mapper.Map<Author>(_context.Authors.Find(id));

        _context.Authors.Remove(author);
        _context.SaveChanges();
        return Ok();
    }
}