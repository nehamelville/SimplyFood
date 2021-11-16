using System;
using Microsoft.AspNetCore.Mvc;
using SimplyFood.Models;

namespace SimplyFood.Controllers
{
    public class FavoriteController:Controller
    {
        private readonly IRecipeRepository _repo;
        private readonly string currentUserId = "abc@test.com"; //TODO: dummy, remove after impl login
        public FavoriteController(IRecipeRepository repo)
        {
            this._repo = repo;
        }
        public IActionResult Index()
        {
            var favorites=_repo.GetFavorites(currentUserId);
            return View(favorites);
        }

        public IActionResult ViewFavorite(int id)
        {
            var favorite =_repo.GetFavorite(id);
            return View(favorite);
        }
        public IActionResult InsertFavoriteToDatabase(Recipe recipeToInsert)
        {
            _repo.InsertFavoriteRecipe(recipeToInsert, currentUserId);
            recipeToInsert.IsFavorite = true;
            return RedirectToAction("ViewFavorite", new { id = recipeToInsert.RecipeId});
        }
        public IActionResult DeleteFavoriteFromDatabase(Recipe currentRecipe)
        {
           _repo.DeleteFavorite(currentRecipe.RecipeId, currentUserId);
            currentRecipe.IsFavorite = false;
            return RedirectToAction("Index");
        }

        public IActionResult ToggleFavorite(Recipe currentRecipe)
        {
            //Remove Favorite
            if (currentRecipe.IsFavorite)
            {
                return DeleteFavoriteFromDatabase(currentRecipe);
            }
            else //Save Favorite
            {
                return InsertFavoriteToDatabase(currentRecipe);
            }

        }

    }
}
