using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaBlazor.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBooksWithStockAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImagenUrl", "Stock" },
                values: new object[] { "images/covers/cien-anos.jpg", 50 });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImagenUrl", "Stock" },
                values: new object[] { "images/covers/don-quijote.jpg", 30 });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImagenUrl", "Stock" },
                values: new object[] { "images/covers/1984.jpg", 75 });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "Autor", "Descripcion", "ImagenUrl", "Precio", "Stock", "Titulo" },
                values: new object[,]
                {
                    { 4, "J.R.R. Tolkien", "Una épica de alta fantasía sobre la lucha contra el Señor Oscuro Sauron.", "images/covers/senor-anillos.jpg", 25.00m, 40, "El Señor de los Anillos" },
                    { 5, "Ray Bradbury", "Presenta una sociedad futura donde los libros están prohibidos y son quemados.", "images/covers/fahrenheit-451.jpg", 11.50m, 60, "Fahrenheit 451" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImagenUrl", "Stock" },
                values: new object[] { "images/placeholder.png", 0 });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImagenUrl", "Stock" },
                values: new object[] { "images/placeholder.png", 0 });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImagenUrl", "Stock" },
                values: new object[] { "images/placeholder.png", 0 });
        }
    }
}
