using la_mia_pizzeria_static.Migrations;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{

    public class PizzaController : Controller
    {
       //INDEX..../index...................................................................
        [HttpGet]
        public IActionResult Index()
        {
            using(PizzeriaDbContext Pizzeria=new PizzeriaDbContext())
            {
                List<Pizza> Pizzas= Pizzeria.Pizzas.ToList();
                return View(Pizzas);

            }
        }

        //SHOW...../ShowDetail?Id=<id>...........................................................
        [HttpGet]
        public IActionResult ShowDetails(int Id)
        {
            using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
            {
                try 
                {
                    Pizza Pizza = Pizzeria.Pizzas.Where(p => p.Id == Id)
                        .Include(Pizza => Pizza.Category)
                        .Include(Pizza=>Pizza.Ingredients)
                        .FirstOrDefault();
                    return View("ShowDetails",Pizza);
                }
                catch 
                {
                    string message = $"nessuna pizza trovata con id= {Id}";
                    return View("Error", message);
                }     
            }
        }
        //CREATE...../Create...........................................................
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
            {
                PizzaForm FormModel = new PizzaForm();
                FormModel.Pizza= new Pizza();
                FormModel.Categories = Pizzeria.Categories.ToList();  
                

                List <SelectListItem> IngredientsList = new List<SelectListItem>();

                foreach (var ing in Pizzeria.Ingredients.ToList())
                {
                    IngredientsList.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString() });
                }
                FormModel.Ingredients = IngredientsList;//tutta sta cosa deve poter diventare una funzione
                return View("Create", FormModel);
            } 
        }


        //STORE...../Create...........................................................
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm Request)
        {
            if (!ModelState.IsValid)
            {

                using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
                {
                    Request.Categories = Pizzeria.Categories.ToList();
                    List <SelectListItem>IngredientsLIst = new List<SelectListItem>();
                    foreach (var ing in Pizzeria.Ingredients.ToList())
                    {
                        IngredientsLIst.Add(new SelectListItem() { Text= ing.Name, Value = ing.Id.ToString() });
                    }
                    Request.Ingredients = IngredientsLIst;
                    return View("Create", Request);
                }   
            }
            else
            {
                using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
                {  
                    Pizza PizzaToCreate= (new Pizza(Request.Pizza.Name, Request.Pizza.Description, Request.Pizza.Img, Request.Pizza.Price, Request.Pizza.CategoryId));
                    if(Request.SelectedIngredients != null)
                    {
                        foreach (string selectedIngId in Request.SelectedIngredients)
                        {
                            int selectedIntIngId= int.Parse(selectedIngId);
                            Ingredient ing =Pizzeria.Ingredients
                                .Where(i=>i.Id == selectedIntIngId)
                                .FirstOrDefault();
                            PizzaToCreate.Ingredients.Add(ing);
                        }
                    }
                    Pizzeria.Pizzas.Add(PizzaToCreate);
                    Pizzeria.SaveChanges();  
                    return RedirectToAction("Index");   
                }          
            }
        }

        //EDIT...../Edit?Id=<id>...........................................................
        [HttpGet]
        public IActionResult Edit(int Id)    
        {
                using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
            {
                Pizza? pizzaToEdit = Pizzeria.Pizzas.Where(p => p.Id == Id)
                    .Include(Pizza=>Pizza.Ingredients)
                    .FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    string message = $"nessuna pizza trovata con id= {Id}";
                    return View("Error", message);
                }
                else
                {
                    PizzaForm FormModel = new PizzaForm();
                    FormModel.Pizza =pizzaToEdit;
       
                    //foreach (var ing in Pizzeria.Ingredients.ToList())
                    //{
                    //    Ingredientslist.Add(new SelectListItem() 
                    //    { Text = ing.Name,
                    //        Value = ing.Id.ToString(),
                    //        //selected
                    //        Selected = pizzaToEdit.Ingredients.Any(i => i.Id == ing.Id)
                    //    });
                    //}

                    List<SelectListItem> IngredientsList = new List<SelectListItem>();

                    foreach (var ing in Pizzeria.Ingredients.ToList())
                    {
                        IngredientsList.Add(new SelectListItem() { Text = ing.Name, Value = ing.Id.ToString(), Selected= pizzaToEdit.Ingredients.Any(i=>i.Id==ing.Id)
                           });
                       // Selected = profileToEdit.Books.Any(m => m.Id == book.Id)
                    }
                    FormModel.Categories = Pizzeria.Categories.ToList();
                    FormModel.Ingredients = IngredientsList;
                    return View(FormModel);
                }
            }
        }

        //UPDATE...../Edit...........................................................
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaForm Request)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
                {
                    List<SelectListItem>ingredientsList= new List<SelectListItem>();

                    foreach (var ing in Pizzeria.Ingredients.ToList())
                    {
                        ingredientsList.Add(new SelectListItem() { Text=ing.Name, Value =ing.Id.ToString() });   
                    }
                    Request.Ingredients= ingredientsList;
                    Request.Categories = Pizzeria.Categories.ToList();
                    return View(Request);
                }
            }

            using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
            {
                Pizza pizzaToEdit = Pizzeria.Pizzas
                    .Where(p => p.Id == id)
                    .Include(p => p.Category)
                    .Include(p=>p.Ingredients)
                    .FirstOrDefault();

                pizzaToEdit.Ingredients.Clear();

                if(Request.SelectedIngredients!=null & pizzaToEdit != null)
                {
                    foreach (string SelectedIngId in Request.SelectedIngredients) 
                    {
                        int selectedIntIngId= int.Parse(SelectedIngId);
                        Ingredient ing =Pizzeria.Ingredients
                            .Where(i=>i.Id==selectedIntIngId)
                            .FirstOrDefault();
                        pizzaToEdit.Ingredients.Add(ing);
                    }
                    pizzaToEdit.Name = Request.Pizza.Name;
                    pizzaToEdit.Description = Request.Pizza.Description;
                    pizzaToEdit.Price = Request.Pizza.Price;
                    pizzaToEdit.Img = Request.Pizza.Img;
                    pizzaToEdit.CategoryId = Request.Pizza.CategoryId;

                    //Pizzeria.Pizzas.Add(pizzaToEdit);
                    Pizzeria.SaveChanges();

                    return RedirectToAction("Index");//devo aggiungere toast_________________ toast evrywere!!!
                }

                else
                {
                    string message = $"nessuna pizza trovata con id= {id}";
                    return View("Error", message);
                }
            }
        }

        //DELETE................................................................
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Destroy(int Id)
        {
            using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
            {
                Pizza? pizzaToDestroy = Pizzeria.Pizzas.FirstOrDefault(p => p.Id == Id);

                if (pizzaToDestroy != null)
                {
                    Pizzeria.Pizzas.Remove(pizzaToDestroy);

                    Pizzeria.SaveChanges();
                    return RedirectToAction("Index");//devo aggiungere toast_________________ toast evrywere!!!
                }
                else
                {
                    string message = $"nessuna pizza trovata con id= {Id}";
                    return View("Error", message);
                }
            }
        }

        //1 aggiungere toast
        //2 fare un'unica vista per il form
        //3 studiare many to many che non va nel create

    }


}
