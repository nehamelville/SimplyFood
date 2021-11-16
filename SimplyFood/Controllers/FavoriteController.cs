using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplyFood.Areas.Identity.Data;
using SimplyFood.Models;

namespace SimplyFood.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IRecipeRepository _repo;
        private UserManager<SimplyFoodUser> _userManager;
        public FavoriteController(IRecipeRepository repo, UserManager<SimplyFoodUser> userManager)
        {
            this._repo = repo;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var favorites = _repo.GetFavorites(GetCurrentUser());
            return View(favorites);
        }

        public IActionResult ViewFavorite(int id)
        {
            var favorite = _repo.GetFavorite(id, GetCurrentUser());
            return View(favorite);
        }
        public IActionResult InsertFavoriteToDatabase(Recipe recipeToInsert)
        {
            _repo.InsertFavoriteRecipe(recipeToInsert, GetCurrentUser());
            recipeToInsert.IsFavorite = true;
            return RedirectToAction("ViewFavorite", new { id = recipeToInsert.RecipeId });
        }
        public IActionResult DeleteFavoriteFromDatabase(Recipe currentRecipe)
        {
            _repo.DeleteFavorite(currentRecipe.RecipeId, GetCurrentUser());
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

        public string GetCurrentUser()
        {
            return _userManager.GetUserId(this.User);
        }

    }
}
