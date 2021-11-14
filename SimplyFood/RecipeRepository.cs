using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using SimplyFood.Models;

namespace SimplyFood
{
    public class RecipeRepository:IRecipeRepository
    {
        private readonly string apiKey;

        public RecipeRepository(String key)
        {
            apiKey = key;
        }

        public IEnumerable<Recipe> GetRecipes(string userInput)
        {
            var client = new RestClient($"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/search?query={ userInput }&offset=0&page=1&r=json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", apiKey);
            IRestResponse response = client.Execute(request);

            var baseUri = JObject.Parse(response.Content).GetValue("baseUri");
            var recipes = JObject.Parse(response.Content).GetValue("results");

            var recipeList = new List<Recipe>();

            if (recipes == null)
            {
                return null;
            }

            foreach (var rcp in recipes)
            {
                var recipe = new Recipe();
                recipe.Id = (int)rcp["id"];
                recipe.ImageUrl = baseUri.ToString() + "/" + (string)rcp["image"];
                recipe.ReadyInMinutes = (int)rcp["readyInMinutes"];
                recipe.Title = (string)rcp["title"];
                //recipe.ImageURLs = (IEnumerable<string>)rcp["imageUrls"];
                recipeList.Add(recipe);
            }

            return recipeList;
        }

        public Recipe GetRecipeInfo(string id)
        {
            var client = new RestClient($"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/{ id }/information");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", apiKey);
            IRestResponse response = client.Execute(request);

            string responseStr = response.Content;
            var recipe = JObject.Parse(responseStr);
            var ingredients = recipe.GetValue("extendedIngredients");
            //var recipeInfo = new RecipeInfo();
            var recipeInfo = new Recipe();
            //var ingredientList = new List<Ingredient>();
            var ingStringBuilder = new StringBuilder();

            recipeInfo.Id = (int)recipe["id"];
            recipeInfo.ImageUrl = (string)recipe["image"];
            recipeInfo.Title = (string)recipe["title"];
            recipeInfo.ReadyInMinutes = (int)recipe["readyInMinutes"];
            recipeInfo.Instructions = (string)recipe["instructions"];
            

            foreach (var ing in ingredients)
            {
                ingStringBuilder.AppendLine((string)ing["originalString"]);
                //var ingredient = new Ingredient();
                //ingredient.Id = (int)ing["id"];
                //ingredient.Aisle = (string)ing["aisle"];
                //ingredient.Name = (string)ing["name"];
                //ingredient.Unit = (string)ing["unit"];
                //ingredient.Amount = (double)ing["amount"];
                //ingredient.Image = (string)ing["image"]; 
                
            }
            recipeInfo.Ingredients = ingStringBuilder.ToString().Trim();

            return recipeInfo;
        }
    }
}
