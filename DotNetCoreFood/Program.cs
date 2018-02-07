using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCoreFood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Default builder:
        /// 1) uses Kestrel web server
        /// 2) enables IIS integration
        /// 3) sets up default logging to command line console / VS2017 Output window
        /// 4) makes IConfiguration service available, using (whichever it finds first):
        ///     - command line arguments
        ///     - environment variables
        ///     - user secrets
        ///     - appsettings.json (create via Add | New Item | ASP.NET Configuration File)
        /// </summary>
        /// <param name="args"></param>
        /// <returns><see cref="IWebHost"/> implementation.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
