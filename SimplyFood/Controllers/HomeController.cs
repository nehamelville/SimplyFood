using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimplyFood.Models;

namespace SimplyFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly string currentUserId = "abc@test.com"; //TODO: dummy, remove after impl login
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeRepository _repo;

        public HomeController(ILogger<HomeController> logger, IRecipeRepository repo)
        {
            this._logger = logger;
            this._repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string userInput)
        {
            var recipes = _repo.GetRecipes(userInput);
            ViewData["userInput"] = userInput;

            return View(recipes);

        }

        public IActionResult ViewRecipe(string id)
        {
            Recipe recipeInfo;
            try
            {
                recipeInfo = _repo.GetFavorite(Int32.Parse(id));
            }catch (Exception)
            {
                recipeInfo = null;
            }
            if (recipeInfo != null)
            {
                recipeInfo.IsFavorite = true;
            } else
            {
                recipeInfo = _repo.GetRecipeInfo(id);
            }
                
            return View(recipeInfo);
        }

        public IActionResult InsertFavoriteToDatabase(Recipe recipeToInsert)
        {
            _repo.InsertFavoriteRecipe(recipeToInsert, currentUserId);
            recipeToInsert.IsFavorite = true;
            return RedirectToAction("ViewRecipe", new { id = recipeToInsert.RecipeId });
        }
        public IActionResult DeleteFavoriteFromDatabase(Recipe currentRecipe)
        {
            _repo.DeleteFavorite(currentRecipe.RecipeId, currentUserId);
            currentRecipe.IsFavorite = false;
            return RedirectToAction("ViewRecipe", new { id = currentRecipe.RecipeId });
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
