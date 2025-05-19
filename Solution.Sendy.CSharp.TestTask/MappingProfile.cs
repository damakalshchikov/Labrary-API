using AutoMapper;
using Solution.Sendy.CSharp.TestTask.DTOs;
using Solution.Sendy.CSharp.TestTask.DataBase.Models;

namespace Solution.Sendy.CSharp.TestTask;

public class MappingProfile  : Profile
{
    public MappingProfile ()
    {
        // Author из БД (EF Core) <-> AutoMapper
        CreateMap<Author, AuthorDTO>();
        CreateMap<CreateAuthorDTO, Author>();
        CreateMap<UpdateAuthorDTO, Author>();

        // Book из БД (EF Core) <-> AutoMapper
        CreateMap<Book, BookDTO>();
        CreateMap<CreateBookDTO, Book>();
        CreateMap<UpdateBookDTO, Book>();
    }
}