using System;
using System.Collections.Generic;

namespace SimplyFood.Models
{
    public class Recipe
    {
        public Recipe()
        {
        }
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public int ReadyInMinutes { get; set; }
        public string ImageUrl { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public Boolean IsFavorite { get; set; }
        //public IEnumerable<Ingredient> Ingredients { get; set; }
        

    }
}
