using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using SimplyFood.Models;

namespace SimplyFood
{
    public class RecipeRepository:IRecipeRepository
    {
        //inject Configuration which includes appsettings.json 
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly IDbConnection _conn;

        public RecipeRepository(IConfiguration configuration, IDbConnection conn)
        {
            _configuration = configuration;
            //read api key from appsettings.json inside Configuration object
            _apiKey = _configuration["myApiKey"];
            _conn = conn;
        }
      
        public IEnumerable<Recipe> GetRecipes(string userInput)
        {
            var client = new RestClient($"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/search?query={ userInput }&offset=0&page=1&r=json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", _apiKey);
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
                recipe.RecipeId = (int)rcp["id"];
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
            request.AddHeader("x-rapidapi-key", _apiKey);
            IRestResponse response = client.Execute(request);

            string responseStr = response.Content;
            var recipe = JObject.Parse(responseStr);
            var ingredients = recipe.GetValue("extendedIngredients");
            var recipeInfo = new Recipe();
            //var ingredientList = new List<Ingredient>();
            var ingStringBuilder = new StringBuilder();

            recipeInfo.RecipeId = (int)recipe["id"];
            recipeInfo.ImageUrl = (string)recipe["image"];
            recipeInfo.Title = (string)recipe["title"];
            recipeInfo.ReadyInMinutes = (int)recipe["readyInMinutes"];
            recipeInfo.Instructions = (string)recipe["instructions"];
            

            foreach (var ing in ingredients)
            {
                ingStringBuilder.AppendLine((string)ing["originalString"]);
              
            }
            recipeInfo.Ingredients = ingStringBuilder.ToString().Trim();

            return recipeInfo;
        }
        public IEnumerable<Recipe> GetFavorites(string emailID)
        {
            return _conn.Query<Recipe>("Select * from favorites where userId=@emailID;", new {emailID= emailID });
        }

        public Recipe GetFavorite(int recipeID)
        {
            return _conn.QuerySingle<Recipe>("Select * from favorites where recipeId=@recipeID", new { recipeID = recipeID });

        }
        public void InsertFavoriteRecipe(Recipe recipeToInsert, string userId)
        {
            _conn.Execute("INSERT INTO favorites (recipeId,readyInMinutes,imageUrl,instructions,ingredients,userId,title) VALUES (@recipeId,@readyInMinutes,@imageUrl,@instructions,@ingredients,@userId,@title);",
                new { recipeId = recipeToInsert.RecipeId, readyInminutes=recipeToInsert.ReadyInMinutes, imageUrl=recipeToInsert.ImageUrl, instructions=recipeToInsert.Instructions, ingredients=recipeToInsert.Ingredients, title=recipeToInsert.Title,userId= userId });
        }

        public void DeleteFavorite(Recipe favorite, string userId)
        {
            _conn.Execute("Delete from favorites where recipeId = @id AND userId = @userId;",
                                       new { id = favorite.RecipeId, userId = userId });
            
        }

    }
}
