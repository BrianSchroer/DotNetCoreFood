using Microsoft.Extensions.Configuration;

namespace DotNetCoreFood
{
    public class Greeter : IGreeter
    {
        private readonly IConfiguration _configuration;

        public Greeter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetMessageOfTheDay()
        {
            return _configuration["Greeting"] ?? "Hello, World!";
        }
    }
}