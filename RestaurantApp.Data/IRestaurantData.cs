using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Core;

namespace RestaurantApp.Data 
{
    public interface IRestaurantData 
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant UpdateRestaurant(Restaurant updatedRestaurant);
        Restaurant AddRestaurant(Restaurant newRestaurant);
        int Commit();
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

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                orderby r.Name
                select r;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        Restaurant IRestaurantData.UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);

            if (restaurant != null) 
            {
                restaurant.Name = updatedRestaurant.Name; 
                restaurant.Location = updatedRestaurant.Location; 
                restaurant.Cuisine = updatedRestaurant.Cuisine; 
            }

            return restaurant;
        }

        Restaurant IRestaurantData.AddRestaurant(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        int IRestaurantData.Commit()
        {
            return 0;
        }

    }
}