using System;
using System.Collections.Generic;
using SimplyFood.Models;

namespace SimplyFood
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetRecipes(string userInput);
        public Recipe GetRecipeInfo(string id);

    }
}
