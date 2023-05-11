using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace la_mia_pizzeria_static.Models
{
    public enum DietType {Vegan,Vegetarian,Everyone}
    [Table("ingredients")]
    public class Ingredient :Timestamps
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [Range(5, 225)]
        public string Name { get; set; }

        [Column("type")]
        public DietType Type{ get; set; }=DietType.Everyone;

        [Column("in_stock")]
        public bool InStock { get; set; }
        //relation
        public List<Pizza> Pizzas { get; set; }

        public Ingredient() { }
        public Ingredient(string _name, DietType _type, bool _inStock ) 
        {
            Name = _name;
            Type = _type;
            InStock = _inStock;
        }
    }
}
