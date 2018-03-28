using DotNetCoreFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreFood.ViewComponents
{
    public class GreeterViewComponent : ViewComponent
    {
        private readonly IGreeter _greeter;

        public GreeterViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", _greeter.GetMessageOfTheDay());
        }
    }
}
