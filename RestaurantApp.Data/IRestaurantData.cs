using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Core;

namespace RestaurantApp.Data 
{
    public interface IRestaurantData 
    {
        IEnumerable<Restaurant> GetAllRestaurants();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id= 1, Name= "Pizza Place", Location="New York", Cuisine = CuisineType.Italian },
                new Restaurant { Id= 2, Name= "Rice Cove", Location="Shanghai", Cuisine = CuisineType.Chinese },
                new Restaurant { Id= 3, Name= "Taco Nacho", Location="Mexico City", Cuisine = CuisineType.Mexican },
            };    
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return from r in restaurants
                orderby r.Name
                select r;
        }
    }
}