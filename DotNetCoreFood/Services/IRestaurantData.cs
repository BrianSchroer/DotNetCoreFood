using DotNetCoreFood.Models;
using System.Collections.Generic;

namespace DotNetCoreFood.Services
{
    public interface IRestaurantData 
    {
        IEnumerable<Restaurant> GetAll();
    }
}