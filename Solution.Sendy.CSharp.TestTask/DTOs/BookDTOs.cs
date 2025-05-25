using System.ComponentModel.DataAnnotations;

namespace Solution.Sendy.CSharp.TestTask.DTOs;

public class BookDTO
{
    public int BookId { get; set; }
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
    public AuthorDTO? Author { get; set; }
}

public class CreateBookDTO
{
    [Required(ErrorMessage = "Название книги  обязательно")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Название книги должно быть от 5 до 50 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Id автора обязательно")]
    [Range(1, int.MaxValue, ErrorMessage = "Id автора должен быть больше 0")]
    public int AuthorId { get; set; } = 1;
}

public class UpdateBookDTO
{
    [Required(ErrorMessage = "Название книги  обязательно")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Название книги должно быть от 5 до 50 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Id автора обязательно")]
    [Range(1, int.MaxValue, ErrorMessage = "Id автора должен быть больше 0")]
    public int AuthorId { get; set; } = 1;
}