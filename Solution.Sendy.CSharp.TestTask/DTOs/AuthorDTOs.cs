using System.ComponentModel.DataAnnotations;

namespace Solution.Sendy.CSharp.TestTask.DTOs;

public class AuthorDTO
{
    public int AuthorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }
}

public class CreateAuthorDTO
{
    [Required(ErrorMessage = "Имя автора  обязательно")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя автора должно быть от 2 до 50 символов")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилия автора  обязательна")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия автора должна быть от 2 до 50 символов")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email  обязателен")]
    [EmailAddress(ErrorMessage = "Email должен быть валидным")]
    public string Email { get; set; } = string.Empty;
}

public class UpdateAuthorDTO
{
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя автора должно быть от 2 до 50 символов")]
    public string? FirstName { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия автора должна быть от 2 до 50 символов")]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "Email должен быть валидным")]
    public string? Email { get; set; }
}