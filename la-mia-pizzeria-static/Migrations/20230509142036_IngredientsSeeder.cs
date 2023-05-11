using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class IngredientsSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
            table: "ingredients",
            columns: new[] { "name", "type", "in_stock", "created_at" },
            values: new object[,]
       {
            { "mozzarella", (int)DietType.Vegetarian, true, DateTime.UtcNow },
            { "tomato", (int)DietType.Vegan, true, DateTime.UtcNow },
            { "bacon", (int)DietType.Everyone, true,DateTime.UtcNow },
            { "balsamic vinegar", (int)DietType.Vegan, true,DateTime.UtcNow },
            { "sausages", (int)DietType.Everyone, true,DateTime.UtcNow },
            { "onions", (int)DietType.Vegan, true , DateTime.UtcNow},
            { "tuna", (int)DietType.Everyone, true , DateTime.UtcNow},
            { "ham", (int)DietType.Everyone, true , DateTime.UtcNow},
            { "potatoes", (int)DietType.Vegan, true , DateTime.UtcNow} ,
            { "zuchinis", (int)DietType.Vegan, true , DateTime.UtcNow},
            { "eggplant", (int)DietType.Vegan, true , DateTime.UtcNow},
            { "spicy salami", (int)DietType.Everyone, true , DateTime.UtcNow},
            { "spinach", (int)DietType.Vegan, true , DateTime.UtcNow},
            { "brie", (int)DietType.Vegetarian, true , DateTime.UtcNow},
            { "gorgonzola", (int)DietType.Vegetarian, true , DateTime.UtcNow},
            { "nuts", (int)DietType.Vegan, true , DateTime.UtcNow}
       });

        }
  

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("" +
            "DELETE FROM ingredients" +
            " WHERE name IN ('mozzarella', 'tomato', 'bacon', 'balsamic vinegar', 'sausages', 'onions', 'tuna', 'ham', 'potatoes', 'zuchinis', 'eggplant', 'spicy salami', 'spinach', 'brie', 'gorgonzola', 'nuts')");
    }//IN include più valori, senza dover ripere where name=a||name=b ecc
    }
}