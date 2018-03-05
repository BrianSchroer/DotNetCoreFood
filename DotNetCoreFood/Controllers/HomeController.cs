using DotNetCoreFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            var model = _restaurantData.GetAll(); 

            return View(model);
        }
    }
}
