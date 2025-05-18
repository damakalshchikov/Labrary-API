# Ход выполнения тестового задания
Данный репозиторий реализует возможное решения [тестового задания](https://github.com/SendyChat/Sendy.CSharp.TestTask/tree/main) на позицию C# Back-end developer.

## Модели
В файлах [`Author.cs`](Solution.Sendy.CSharp.TestTask/DataBase/Models/Author.cs) и [`Book.cs`](Solution.Sendy.CSharp.TestTask/DataBase/Models/Book.cs) определены модели, по которым будут создаваться сущности в базе данных.

## Настройка EF Core
### DbContext
Файл [`DatabaseContext.cs`](Solution.Sendy.CSharp.TestTask/DataBase/DatabaseContext.cs) содержит определение контекста базы данных для взаимодействия с ней.

### Миграции
Миграции содержаться в том же каталоге, что и [`DatabaseContext.cs`](Solution.Sendy.CSharp.TestTask/DataBase/DatabaseContext.cs), в соответствующем каталоге [`Migrations`](Solution.Sendy.CSharp.TestTask/DataBase/Migrations).
В данном репозитории содержаться миграции инициализации базы данных.

### Seeder
Файл [`Seeder.cs`](Solution.Sendy.CSharp.TestTask/DataBase/Seeder.cs) содержит определение класса `Seeder`
для начального засеивания данных в модели `Author` и `Book`.