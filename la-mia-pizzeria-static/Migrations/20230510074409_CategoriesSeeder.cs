using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "name"},
            values: new object[,]
       {
            {"Classic"},
            {"Special"},
            {"Dedicated"}
       });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData( table: "Categories",  keyColumn: "id", keyValues: new object[] { 1, 2, 3 } );

        }
    }
}
