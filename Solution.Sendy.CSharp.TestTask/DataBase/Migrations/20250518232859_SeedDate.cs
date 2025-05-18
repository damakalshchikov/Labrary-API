using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Solution.Sendy.CSharp.TestTask.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class SeedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "pushkin_kolotushkin@alex.org", "Александр", "Пушкин" },
                    { 2, "lionFat@mail.ru", "Лев", "Толстой" },
                    { 3, "londonUK@gmail.com", "Джек", "Лондон" },
                    { 4, "stoicism@yandex.ru", "Марк", "Аврелий" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Сказка о царе Салтане" },
                    { 2, 1, "Евгений Онегин" },
                    { 3, 2, "Война и мир" },
                    { 4, 2, "Анна Каренина" },
                    { 5, 3, "Белый Клык" },
                    { 6, 3, "Любовь к жизни" },
                    { 7, 4, "Наедине с собой" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);
        }
    }
}
