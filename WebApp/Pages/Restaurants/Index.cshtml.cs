using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

using RestaurantApp;
using RestaurantApp.Core;
using RestaurantApp.Data;

namespace RestaurantApp.Pages.Restaurants 
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly RestaurantContext _context;

        // [BindProperty(SupportsGet = true)]
        public string NameSort { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }

        public string Message { get; set; }
        public PaginatedList<Restaurant> Restaurants { get; set; }

        public IndexModel(IConfiguration config, RestaurantContext restaurantContext)
        {
            this._config = config;
            this._context = restaurantContext;
        }

        public async Task OnGetAsync( string sortOrder,
            string currentFilter, string searchTerm, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchTerm != null)
            {
                pageIndex = 1;
            }
            else 
            {
                searchTerm = currentFilter;
            }

            CurrentFilter = searchTerm;

            IQueryable<Restaurant> restaurantIQ = from r in _context.Restaurants select r;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                restaurantIQ = restaurantIQ.Where(r => r.Name.Contains(searchTerm));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    restaurantIQ = restaurantIQ.OrderByDescending(r => r.Name);
                    break;
                default:
                    restaurantIQ = restaurantIQ.OrderBy(r => r.Name);
                    break;
            }

            var pageSize = _config.GetValue("PageSize", 5);
            Restaurants = await PaginatedList<Restaurant>.CreateAsync(
                restaurantIQ.AsNoTracking(), pageIndex ?? 1, pageSize
            );
        }
    }
}