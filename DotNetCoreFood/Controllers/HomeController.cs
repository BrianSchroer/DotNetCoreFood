using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using DotNetCoreFood.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreFood.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public virtual IActionResult Index()
        {
            return base.View(new HomeIndexViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetMessageOfTheDay()
            });
        }

        public virtual IActionResult Details(int id)
        {
            Restaurant model = _restaurantData.Get(id);

            if (model == null)
            {
                return RedirectToAction(ActionNameConstants.Index);
            }

            return View(model);
        }
    }
}
