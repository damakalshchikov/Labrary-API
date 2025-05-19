using System.ComponentModel.DataAnnotations;

namespace Solution.Sendy.CSharp.TestTask.DTOs;

public class AuthorDTO
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class CreateAuthorDTO
{
    [Required(ErrorMessage = "Имя автора - обязательное поле для заполнения")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Фамилия автора - обязательное поле заполнения")]
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}

public class UpdateAuthorDTO
{
    [Required(ErrorMessage = "Имя автора - обязательное поле для заполнения")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Фамилия автора - обязательное поле для заполнения")]
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}