using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using RestaurantApp.Core;
using RestaurantApp.Data;

namespace RestaurantApp.Pages.Restaurants 
{
    public class DetailModel : PageModel
    {
        private readonly RestaurantApp.Data.RestaurantContext _context;

        [TempData]
        public string Message { get; set; }
        public Restaurant Restaurant { get; set; } 

        public DetailModel(RestaurantApp.Data.RestaurantContext restaurantContext)
        {
            this._context = restaurantContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurants
                .Include(r => r.Dishes)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Restaurant == null)
                return NotFound();

            return Page();
        }
    }
}