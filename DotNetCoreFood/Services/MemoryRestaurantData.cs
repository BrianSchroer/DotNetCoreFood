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
                new Restaurant { Id = 1, Name = "Sparky's Pizza"},
                new Restaurant { Id = 2, Name = "Tersiguels"},
                new Restaurant { Id = 3, Name = "King's Contrivance"}
            };
        }
        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }
    }
}
