using System.ComponentModel.DataAnnotations;

namespace Solution.Sendy.CSharp.TestTask.DTOs;

public class BookDTO
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public int AuthorId { get; set; }
    public AuthorDTO Author { get; set; } = null!;
}

public class CreateBookDTO
{
    [Required(ErrorMessage = "Название книги - обязательное поле для заполнения")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "ID автора - обязательное поле для заполнения")]
    public int AuthorId { get; set; }
}

public class UpdateBookDTO
{
    [Required(ErrorMessage = "Название книги - обязательное поле для заполнения")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "ID автора - обязательное поле для заполнения")]
    public int AuthorId { get; set; }
}