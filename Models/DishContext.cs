using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CRUDelicious.Models;


namespace CRUDelicious.Models
{
    public class DishContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DishContext(DbContextOptions<DishContext> options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
    }
}