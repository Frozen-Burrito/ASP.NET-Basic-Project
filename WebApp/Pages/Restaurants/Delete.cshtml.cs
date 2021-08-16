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
    public class DeleteModel : PageModel
    {
        private readonly RestaurantContext _context;

        public DeleteModel(RestaurantContext restaurantContext)
        {
            this._context = restaurantContext;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public string ErrMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesErr = false)
        {
            if (id == null) return NotFound();

            Restaurant = await _context.Restaurants
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Restaurant == null) return NotFound();

            if (saveChangesErr.GetValueOrDefault())
            {
                ErrMessage = String.Format("Delete {ID} failed. Try again.", id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null) return NotFound();

            try
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction("./Delete",
                                        new { id, saveChangesErr = true });
            }
        }
    }
}