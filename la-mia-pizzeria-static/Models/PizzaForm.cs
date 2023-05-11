using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaForm
    {
        public Pizza Pizza { get; set; }
        public List <Category>? Categories { get; set; }
        //public List <Ingredient> Ingredients { get; set;}
        public List <SelectListItem>? Ingredients { get; set; }//elenco ingredienti selezionabili
        public List <string>? SelectedIngredients { get; set; }//id libri selezionati
    }
}
