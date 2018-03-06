using DotNetCoreFood.Models;
using System.Collections.Generic;

namespace DotNetCoreFood.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public string CurrentMessage { get; set; }
    }
}
