using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.' 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="configuration">Not added by default. 
        /// I added it manually. Dependency injection knows what to do with it.</param>
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                string greeting = configuration["Greeting"] ?? "Hello, World!";
                await context.Response.WriteAsync(greeting);
            });
        }
    }
}
