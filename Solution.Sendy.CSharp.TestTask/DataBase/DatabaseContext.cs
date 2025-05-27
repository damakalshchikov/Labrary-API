using Solution.Sendy.CSharp.TestTask.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Solution.Sendy.CSharp.TestTask.DataBase;

public class DatabaseContext : DbContext
{
    // Создаём таблицы по моделям
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    // Базовая настройка контекста данных
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    // Метод конфигурации моделей
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настраиваем отношение между Book и Author
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        Seeder.SeedAuthors(modelBuilder); // Засеиваем начальные данные в Author
        Seeder.SeedBooks(modelBuilder); // Засеиваем начальные данные в Book
    }
}