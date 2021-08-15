using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RestaurantApp.Core;
using RestaurantApp.Data;

// #nullable enable

namespace RecipeApp.Pages.Restaurants 
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData,
                        IHtmlHelper htmlHelper)
        {
            this._restaurantData = restaurantData;
            this._htmlHelper = htmlHelper;
        }
        
        public ActionResult OnGet(int? restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = _restaurantData.GetById(restaurantId.Value);
            }
            else 
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant is null) 
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0) 
            {
                _restaurantData.UpdateRestaurant(Restaurant);
            }
            else 
            {
                _restaurantData.AddRestaurant(Restaurant);
            }
            
            _restaurantData.Commit();

            TempData["Message"] = "Restaurant changes saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            
        }
    }
}