using System.Collections.Generic;
using System.Linq;
using DotNetCoreFood.Models;

namespace DotNetCoreFood.Services
{
    public class MemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> _restaurants;

        public MemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Sparky's Pizza", Cuisine=CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Tersiguels" },
                new Restaurant { Id = 3, Name = "King's Contrivance"}
            };
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            _restaurants.Remove(Get(restaurant.Id));
            _restaurants.Add(restaurant);
            return restaurant;
        }
    }
}
