using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private DishContext dbContext;
        //injecting context service
        public HomeController(DishContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> alldishes = dbContext.Dishes.ToList();
            ViewBag.all = alldishes;
            // foreach(var d in ViewBag.all){
            //     Console.WriteLine(d.DishName);
            // }
            return View();
        }
        [HttpGet("new")]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost("Add")]
        public IActionResult AddDish(Dish dish)
        {
            if (ModelState.IsValid)
            {
                Dish newdish = dish;
                dbContext.Add(newdish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("NOT VALID**************", ModelState.Keys);
                foreach (var error in ModelState)
                {
                    if (error.Value.Errors.Count > 0)
                    {
                        Console.WriteLine("err msg " + error.Value.Errors[0].ErrorMessage);
                    }
                }
                return RedirectToAction("Add");
            }
        }

        [HttpGet("{DishId}")]
        public IActionResult OneDish(int DishId)
        {
            ViewBag.onedish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
            //can also use Model like so:
            // Dish onedish = dbContext.dishes.FirstOrDefault(d => d.DishId == DishId);
            //return View("OneDish", onedish)
            //in Razor file:
            //@Model.DishName
            return View("OneDish");
        }

        [HttpGet("delete/{DishId}")]
        public IActionResult Delete(int DishId)
        {
            Dish dish = dbContext.Dishes.SingleOrDefault(d => d.DishId == DishId);
            dbContext.Dishes.Remove(dish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/edit/{DishId}")]
        public IActionResult Edit(int DishId)
        {
            ViewBag.onedish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
            return View("Edit");
        }

        [HttpPost("ProcessEdit/{DishId}")]
        public IActionResult ProcessEdit(Dish dish, int DishId)
        {
            if (ModelState.IsValid)
            {
                Dish olddish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
                Dish updateddish = dish;
                olddish.Name = updateddish.Name;
                olddish.Chef = updateddish.Chef;
                olddish.Calories = updateddish.Calories;
                olddish.Tastiness = updateddish.Tastiness;
                olddish.Description = updateddish.Description;
                olddish.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();

                ViewBag.onedish = updateddish;
                return RedirectToAction("OneDish");
            }
            else
            {
                Console.WriteLine("NOT VALID**************", ModelState.Keys);
                foreach (var error in ModelState)
                {
                    if (error.Value.Errors.Count > 0)
                    {
                        Console.WriteLine("err msg " + error.Value.Errors[0].ErrorMessage);
                    }
                }
                return RedirectToAction("Edit");
            }
        }

    }
}
