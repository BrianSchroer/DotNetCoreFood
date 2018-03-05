using System.Collections.Generic;
using System.Linq;
using DotNetCoreFood.Models;

namespace DotNetCoreFood.Services
{
    public class MemoryRestaurantData : IRestaurantData
    {
        public IEnumerable<Restaurant> GetAll()
        {
            return new[]
            {
                new Restaurant { Id = 1, Name = "Sparky's Pizza"},
                new Restaurant { Id = 2, Name = "Tersiguels"},
                new Restaurant { Id = 3, Name = "King's Contrivance"}
            }
            .OrderBy(r => r.Name);
        }
    }
}
