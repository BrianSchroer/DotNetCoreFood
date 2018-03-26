using DotNetCoreFood.Models;
using DotNetCoreFood.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCoreFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restauranttData;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public EditModel(IRestaurantData restaurantData)
        {
            _restauranttData = restaurantData;
        }

        public IActionResult OnGet(int id)
        {
            Restaurant = _restauranttData.Get(id);

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
                _restauranttData.Update(Restaurant);
                return RedirectToAction(MVC.Home.ActionNames.Details, MVC.Home.Name, new { id = Restaurant.Id } );
            }

            return Page();
        }
    }
}