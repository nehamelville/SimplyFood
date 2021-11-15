using System;
using System.Collections.Generic;
using SimplyFood.Models;

namespace SimplyFood
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetRecipes(string userInput);
        public Recipe GetRecipeInfo(string id);
        public IEnumerable<Recipe> GetFavorites(string emailID);
        public Recipe GetFavorite(int id);
        public void InsertFavoriteRecipe(Recipe recipeToInsert, string userid);
        public void DeleteFavorite(Recipe favorite, string userid);

    }
}
