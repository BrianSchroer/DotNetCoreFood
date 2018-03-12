using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using DotNetCoreFood.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SparkyTools.AutoMapper;

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

        [HttpGet]
        public virtual IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(RestaurantEditModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = _restaurantData.Add(model.MappedTo<Restaurant>());

                // POST Redirect GET pattern
                return RedirectToAction(Actions.Details(restaurant.Id));
            }

            return View();
        }
    }
}
