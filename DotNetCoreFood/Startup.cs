using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Dependencies are injected automatically. Use the <see cref="ConfigureServices(IServiceCollection)"/>
        /// method above to register custom dependencies.
        /// </summary>
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                string greeting = greeter.GetMessageOfTheDay() ?? "Hello, World!";
                await context.Response.WriteAsync(greeting);
            });
        }
    }
}
