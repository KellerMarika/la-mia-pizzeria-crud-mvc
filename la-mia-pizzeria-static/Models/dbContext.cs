using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }

        public string connectionString = "Data Source = localhost; Initial Catalog = db_pizzeria; Integrated Security = True;TrustServerCertificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connectionString).LogTo(s => Debug.WriteLine(s));
    } 
}