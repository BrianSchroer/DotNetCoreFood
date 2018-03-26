using DotNetCoreFood.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCoreFood.Pages
{
    public class GreetingModel : PageModel
    {
        private readonly IGreeter _greeter;

        public string CurrentGreeting { get; private set; }

        public string Name { get; private set; }

        public GreetingModel(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public void OnGet(string name)
        {
            Name = name;
            CurrentGreeting = _greeter.GetMessageOfTheDay();
        }
    }
}