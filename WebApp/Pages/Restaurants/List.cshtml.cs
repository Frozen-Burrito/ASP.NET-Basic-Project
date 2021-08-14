using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RestaurantApp.Core;
using RestaurantApp.Data;

namespace RecipeApp.Pages.Restaurants 
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IRestaurantData _restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this._config = config;
            this._restaurantData = restaurantData;
        }

        public void OnGet()
        {
            Message = _config["Message"];   
            Restaurants = _restaurantData.GetAllRestaurants(); 
        }
    }
}