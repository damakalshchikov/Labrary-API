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
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
}

public class UpdateBookDTO
{
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
}