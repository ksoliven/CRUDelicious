using System;
using System.ComponentModel.DataAnnotations;


namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required  (ErrorMessage ="Dish can not be empty.")]
        [MinLength(4, ErrorMessage = "Dish must be at least 4 characters or more.")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Chef name can not be empty.")]
        [MinLength(4, ErrorMessage ="Chef must be at least 4 characters or more.")]
        public string Chef { get; set; }
        [Required (ErrorMessage ="Tastiness can not be blank.")]
        public int Tastiness { get; set; }
        [Required (ErrorMessage ="Calories can not be blank.")]
        [Range(1, 2000, ErrorMessage = "Calories must be between 1 and 2000.")]
        public string Calories { get; set; }
        [Required (ErrorMessage ="Description can not be blank")]
        public string Description {get; set;}
        // The MySQL DATETIME type can be represented by a DateTime
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        }
    }
