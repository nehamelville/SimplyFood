using System;
using System.Collections.Generic;
using SimplyFood.Models;

namespace SimplyFood
{
    public interface IRecipeRepository
    {
        public IEnumerable<Recipe> GetRecipes(string userInput);
        public Recipe GetRecipeInfo(string id);
        public IEnumerable<Recipe> GetFavorites(string userID);
        public Recipe GetFavorite(int id, string userID);
        public void InsertFavoriteRecipe(Recipe recipeToInsert, string userid);
        public void DeleteFavorite(int recipeId, string userid);

    }
}