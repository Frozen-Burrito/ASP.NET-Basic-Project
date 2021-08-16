using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using RestaurantApp.Core;
using RestaurantApp.Data;

namespace RestaurantApp.Pages.Restaurants
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantContext _context;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public CreateModel(RestaurantContext restaurantContext, IHtmlHelper htmlHelper)
        {
            this._context = restaurantContext;
            this._htmlHelper = htmlHelper;
        } 

        public IActionResult OnGet()
        {
            Restaurant = new Restaurant();
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyRestaurant = new Restaurant();

            if (await TryUpdateModelAsync<Restaurant>(
                emptyRestaurant,
                "restaurant",
                r => r.Name, r => r.Location, r => r.Cuisine))
            {
                _context.Restaurants.Add(emptyRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }
    }
}