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
        //inject Configuration which includes appsettings.json 
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeRepository recipeRepository;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            //read api key from appsettings.json inside Configuration object
            _apiKey = _configuration["myApiKey"];
            recipeRepository = new RecipeRepository(_apiKey);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string userInput)
        {
            var recipes = recipeRepository.GetRecipes(userInput);
            ViewData["userInput"] = userInput;

            return View(recipes);

        }

        public IActionResult ViewRecipe(string id)
        {
            var recipeInfo = recipeRepository.GetRecipeInfo(id);

            return View(recipeInfo);
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
