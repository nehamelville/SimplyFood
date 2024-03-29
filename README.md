# SimplyFood
## Overview

It’s a food recipe web app built using ASP.NET and the MVC architectural pattern.

I’ve used the following languages & technologies:
- C# 
- ASP.NET
- MVC
- HTML5
- CSS
- Razor Pages 
- Bootstrap
- MySQL
- RestSharp
- Dapper
- REST API(spoonacular API)
- JSON

With this app, you can search for recipes using the [Spoonacular API](https://rapidapi.com/spoonacular/api/recipe-food-nutrition/), view a list of matched recipes, view information of each recipe including photo, name, prep time, ingredients and instructions. If you register and login to SimplyFood, you can add any recipe to your Favorites collection. Favorites are unique to each user and you can browse all the details of your favorite recipes and also unfavorite them anytime. 

## Challenges

I had a few challenges during this project:
- Adding the Identity feature to the app. Basically I used Visual Studio’s Scaffolding feature to add Identity to my existing project. This generates the Login & Register pages and allows me to modify it later. Identity scaffolding was not supported on Visual Studio for the Mac. So I had to first port the project to my Windows laptop, generate the files and then port it back. But there were DB migration issues because the default SQL Server database was not supported on the Mac. So I had to go back and redo the scaffolding but this time select SQLite DB to store all the User data and then migrate it back again to the Mac. 
- Building the Favorites feature was tricky because I had to manage toggling favorites: adding and removing a recipe from/to the favorites DB.

## Future Enhancements:

- I plan to make the app more responsive especially on mobile.
- Add pagination to view more than 10 recipes at a time. 
- Show a Save Favorite button even for non-registered users. If they tap on the button, they can be prompted to login via popup.
- I also want to handle server & database synchronization so that recipe data is always fresh and up to date. 


##SCREEN SHOTS

_**HOMEPAGE**_

<img width="1439" alt="Screen Shot 2021-11-26 at 10 47 56 AM" src="https://user-images.githubusercontent.com/90794087/194770829-46bc9134-f1c6-46c1-9356-ac8a60126eca.png">

_**SEARCH PAGE**_

<img width="1439" alt="Screen Shot 2021-11-26 at 10 49 52 AM" src="https://user-images.githubusercontent.com/90794087/194770839-30c9f9e7-7fe0-4e5f-adb1-35845b1347dc.png">

<img width="1431" alt="Screen Shot 2021-11-26 at 10 53 31 AM" src="https://user-images.githubusercontent.com/90794087/194770863-a752f244-721f-4436-b621-4c24daa4d783.png">

<img width="1435" alt="Screen Shot 2021-11-26 at 10 54 27 AM" src="https://user-images.githubusercontent.com/90794087/194770894-f105c44c-a704-4c01-972a-1dc4c1bf9c85.png">

<img width="1438" alt="Screen Shot 2021-11-26 at 10 55 06 AM" src="https://user-images.githubusercontent.com/90794087/194770898-02c02f81-f351-4085-9aa3-b6d8b203b736.png">

<img width="1437" alt="Screen Shot 2021-11-26 at 10 55 34 AM" src="https://user-images.githubusercontent.com/90794087/194770900-3ce52417-f229-4cd8-85db-dba6f2f375d0.png">

<img width="1427" alt="Screen Shot 2021-11-26 at 10 56 38 AM" src="https://user-images.githubusercontent.com/90794087/194770906-0870eac9-fdd1-4f1c-97e3-2c01ea78acdc.png">

<img width="1405" alt="Screen Shot 2021-11-26 at 10 57 10 AM" src="https://user-images.githubusercontent.com/90794087/194770908-c4b3b812-5164-432e-83a7-1a03922c184e.png">
