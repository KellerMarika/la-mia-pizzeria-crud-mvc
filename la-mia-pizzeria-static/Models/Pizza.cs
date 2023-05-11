using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;

namespace la_mia_pizzeria_static.Models
{

    [Table("pizzas")]
    public class Pizza : Timestamps
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        private string name;
        [Column("name")]
        [Required]
        [MaximumLength(225)]
        [MinimumLength(3)]
        public string Name 
        { get=> name;
          set=> name= "Pizza" + char.ToUpper(value[0]) + value.Substring(1);
        }

        [Column("description")]
        [Required]
        [MaximumLength(225)]
        [MinimumLength(5)]
        // concatenazione dove si alternano 5 w (word) e 4 s(space)
       // [RegularExpression(@"\w+\s+\w+\s+\w+\s+\w+\s+\w+", ErrorMessage = "min 5 words required")]
        public string Description { get; set; }

        [Column("img")]
        [Required]
        [MaximumLength(225)]
        [MinimumLength(1)]
        public string Img { get; set; }

        [Column("price")]
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public double Price { get; set; }


        //relations
        public List<Ingredient>? Ingredients { get; set; }=new List<Ingredient>() { };

        [Column("category_id")]
        public  int CategoryId { get; set; }
        public Category? Category { get; set; }


        //+timestamps

        public Pizza() { }
        public Pizza(string _name, string _description, string _img, double _price, int _categoryId)
        {
            Name = _name;
            Description = _description;
            Img = _img;
            Price = _price;
            CategoryId = _categoryId;
        }
        public Pizza(string _name, string _description, string _img, double _price, List <Ingredient> _ingredients, int _categoryId)
        {
            Name = _name;
            Description = _description;
            Img = _img;
            Price = _price;
            Ingredients = _ingredients;
            CategoryId = _categoryId;
        }
        public Pizza(string _name, string _description, string _img, double _price, List<Ingredient> _ingredients, Category _categoryId)
        {
            Name = _name;
            Description = _description;
            Img = _img;
            Price = _price;
            Ingredients = _ingredients;
            Category = _categoryId;
        }
    }
}
