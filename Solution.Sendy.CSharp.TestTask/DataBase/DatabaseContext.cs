using Solution.Sendy.CSharp.TestTask.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Solution.Sendy.CSharp.TestTask.DataBase;

public class DatabaseContext : DbContext
{
    // Создаём таблицы по моделям
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
}