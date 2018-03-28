using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCoreFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public EditModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult OnGet(int id)
        {
            Restaurant = _restaurantData.Get(id);

            if (Restaurant == null)
            {
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _restaurantData.Update(Restaurant);
                return RedirectToAction(MVC.Home.ActionNames.Details, MVC.Home.Name, new { id = Restaurant.Id } );
            }

            return Page();
        }
    }
}