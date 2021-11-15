using System;
using Microsoft.AspNetCore.Mvc;
using SimplyFood.Models;

namespace SimplyFood.Controllers
{
    public class FavoriteController:Controller
    {
        private readonly IRecipeRepository _repo;
        private readonly string currentUserId = "abc@test.com"; //TODO: remove after impl login
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
        public IActionResult InsertFavoriteToDatabase(Recipe recipeToInsert, string emailId)
        {
            _repo.InsertFavoriteRecipe(recipeToInsert, emailId);
            return RedirectToAction("Search");
        }
        public IActionResult DeleteFavoriteFromDatabase(Recipe favorite, string emailId)
        {
           _repo.DeleteFavorite(favorite, emailId);
           return RedirectToAction("Favorites");
        }

    }
}
