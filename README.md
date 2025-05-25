# Содержание
Данный репозиторий реализует возможное решения [тестового задания](https://github.com/SendyChat/Sendy.CSharp.TestTask/tree/main) на позицию C# Back-end developer.

## Модели
В файлах [`Author.cs`](Solution.Sendy.CSharp.TestTask/DataBase/Models/Author.cs) и [`Book.cs`](Solution.Sendy.CSharp.TestTask/DataBase/Models/Book.cs) определены модели, по которым будут создаваться сущности в базе данных.

## Настройка EF Core
### DbContext
Файл [`DatabaseContext.cs`](Solution.Sendy.CSharp.TestTask/DataBase/DatabaseContext.cs) содержит определение контекста базы данных для взаимодействия с ней.

### Миграции
Миграции содержаться в том же каталоге, что и [`DatabaseContext.cs`](Solution.Sendy.CSharp.TestTask/DataBase/DatabaseContext.cs), в соответствующем каталоге [`Migrations`](Solution.Sendy.CSharp.TestTask/DataBase/Migrations).

### Seeder
Файл [`Seeder.cs`](Solution.Sendy.CSharp.TestTask/DataBase/Seeder.cs) содержит определение класса `Seeder`
для начального засеивания данных в модели `Author` и `Book`.

## AutoMapper
Для маппинга были реализованы 3 `DTO-объекта` для каждой модели - `Author` и `Book`:

### В файле [`AuthorDTOs.cs`](Solution.Sendy.CSharp.TestTask/DTOs/AuthorDTOs.cs):
- `AuthorDTO` - маппинг `Author` <-> `AuthorDTO`;
- `CreateAuthorDTO` - маппинг `CreateAuthorDTO` -> `Author`;
- `UpdateAuthorDTO` - маппинг `UpdateAuthorDTO` -> `Author`.

### В файле [`BookDTOs.cs`](Solution.Sendy.CSharp.TestTask/DTOs/BookDTOs.cs):
- `BookDTO` - маппинг `Book` <-> `BookDTO`;
- `CreateBookDTO` - маппинг `CreateBookDTO` -> `Book`;
- `UpdateBookDTO` - маппинг `UpdateBookDTO` -> `Book`.

## CRUD-эндпоинты
Для взаимодейсвтия с даннымы моделей, были определены контроллеры `AuthorController` и `BookController`. Эти контроллеры реализованы в файлах [`AuthorController.cs`](Solution.Sendy.CSharp.TestTask\Controllers\AuthorController.cs) и [`BookController.cs`](Solution.Sendy.CSharp.TestTask\Controllers\BookController.cs). Каждый из этих контроллеров реализует CRUD-эндпоинты(GET, POST, PUT и DELETE).

## Middleware

### TimingMiddleware
Мидлварь для логирования времени запросов. Определение в файле [`TimingMiddleware.cs`](Solution.Sendy.CSharp.TestTask\Middleware\TimingMiddleware.cs).

### ApiKeyMiddleware
Мидлварь для проверки заголовка `x-api-key`. Определение в файле [`ApiKeyMiddleware.cs`](Solution.Sendy.CSharp.TestTask\Middleware\ApiKeyMiddleware.cs).

## Serilog
Для логирования использовался `Serilog`. Его настройка определена в [`Program.cs`](Solution.Sendy.CSharp.TestTask\Program.cs).

## Swagger/OpenAPI
Для документирования `API` был настроен `Swagger`. По адресу `localhost:5067/swagger` содержится подробная документация всех методов контроллеров.

## Глобальная обработка ошибок
Для глобальной обработки ошибок была определена мидлварь, которая перехватывает исключения и возвращает ответ клиенту в фомате `ProblemDetails`. Мидлварь определна в файле [`ExceptionsHandlerMiddleware.cs`](Solution.Sendy.CSharp.TestTask\Middleware\ExceptionsHandlerMiddleware.cs).

## Валидация DTO
Для валидации входящих данных от клиента использовалась `DataAnnotations`.