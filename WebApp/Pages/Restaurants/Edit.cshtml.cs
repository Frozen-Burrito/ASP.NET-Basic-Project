using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.Core;
using RestaurantApp.Data;

// #nullable enable

namespace RestaurantApp.Pages.Restaurants 
{
    public class EditModel : PageModel
    {
        private readonly RestaurantContext _context;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(RestaurantContext _context,
                        IHtmlHelper htmlHelper)
        {
            this._context = _context;
            this._htmlHelper = htmlHelper;
        }
        
        public async Task<ActionResult> OnGetAsync(int? id)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            Restaurant = await _context.Restaurants
                .Include(r => r.Dishes)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Restaurant is null) 
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (await TryUpdateModelAsync<Restaurant>(
                Restaurant,
                "restaurant",
                r => r.Name, r => r.Location, r => r.Cuisine))
            {
                _context.Restaurants.Update(Restaurant);
                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Restaurant changes saved!";
            return RedirectToPage("./Detail", new { id = Restaurant.Id });
        }
    }
}