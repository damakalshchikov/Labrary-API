using Microsoft.EntityFrameworkCore;
using Solution.Sendy.CSharp.TestTask.DataBase.Models;

namespace Solution.Sendy.CSharp.TestTask.DataBase;

// Сидр для начального заполнения БД данными
public class Seeder
{
    // Метод для засеивания данных в модель Author
    public static void SeedAuthors(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new Author { AuthorId = 1, FirstName = "Александр", LastName = "Пушкин", Email = "pushkin_kolotushkin@alex.org" },
            new Author { AuthorId = 2, FirstName = "Лев", LastName = "Толстой", Email = "lionFat@mail.ru"},
            new Author { AuthorId = 3, FirstName = "Джек", LastName = "Лондон", Email = "londonUK@gmail.com"},
            new Author { AuthorId = 4, FirstName = "Марк", LastName = "Аврелий", Email = "stoicism@yandex.ru"}
        );
    }

    // Метод для засеивания данных в модель Book
    public static void SeedBooks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book {BookId = 1, AuthorId = 1, Title = "Сказка о царе Салтане"},
            new Book { BookId = 2, AuthorId = 1, Title = "Евгений Онегин"},
            new Book { BookId = 3, AuthorId = 2, Title = "Война и мир"},
            new Book { BookId = 4, AuthorId = 2, Title = "Анна Каренина"},
            new Book { BookId = 5, AuthorId = 3, Title = "Белый Клык"},
            new Book { BookId = 6, AuthorId = 3, Title = "Любовь к жизни"},
            new Book { BookId = 7, AuthorId = 4, Title = "Наедине с собой"}
        );
    }
}