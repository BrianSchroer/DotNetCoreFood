ASP.NET Core 2.0 food web app created by following along with [K Scott Allen's **ASP.NET Core Fundamentals** Pluralsight course]ASP.NET Core 2.0 food web app created by following along with [K Scott Allen's **ASP.NET Core Fundamentals** Pluralsight course](https://www.pluralsight.com/courses/aspdotnet-core-fundamentals)...

## What I've learned...

### Startup & Configuration
* ASP.NET Core sites are bootstrapped via [**Program.Main**](DotNetCoreFood/Program.cs).
It calls its static **BuildWebHost** method. **WebHost.CreateDefaultBuilder**
	* uses the Kestrel web server
    * enables IIS integration
    * sets up default logging to the command line console / Visual Studio "Output" window
* `.UseStartup<Startup>` in Program.BuildWebHost specifies that [**Startup.cs**](DotNetCoreFood/Startup.cs)
is to be used for configuration. (See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup.)
* Arguments to **Startup.Configure** are automatically dependency-injected
(as are arguments to the constructors of those dependencies... and their dependencies, etc.)
* Several interfaces (e.g. IApplicationBuilder, IHostingEnvironment, IConfiguration) are pre-registered with default implementations.
You can register your own dependencies (or override defaults) in **Startup.ConfigureServices**.
* Some IServiceCollection.Add methods:
  * AddSingleton
  * AddScoped = one instance per HTTP request
* The default implementation of [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=aspnetcore-2.0) 
is [Microsoft.Extensions.Configuration.ConfigurationRoot](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationroot?view=aspnetcore-2.0).
Its `Item[String}` property (e.g. `_configuration["Greeting"]`) looks at these sources (in order):
	* command line argument
    * environment variable
    * user secret
    * [**appsettings.json**](DotNetCoreFood/appsettings.json)
      * appsettings.*environmentName*.json files can be used to override settings for a specific environment (e.g. "appsettings.development.json").
* Middleware is defined in [Startup.Configure](DotNetCoreFood/Startup.cs) via 
[`IApplicationBuilder.Use...` extension methods](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.iapplicationbuilder?view=aspnetcore-2.0).
The order in which these middleware usages is defined is important.
* [launchsettings.json](DotNetCoreFood/Properties/launchsettings.json) defines environment variables, 
Windows authentication, URL & port, etc. for local machine development.
(Can be viewed/modified via VS2017 GUI via project right-click | Properties| Debug.)
(See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments.)

### Serving static files
* Call **IApplicationBuilder.UseStaticFiles()** from [Startup.Configure](DotNetCoreFood/Startup.cs) to enable serving files from the "wwwroot" folder.
* **IApplicationBuilder.UseDefaultFiles()** says to serve index.html from wwwroot (or index.html from any subfolder)
when the URL only contains the folder name. (It's configurable, but "index.html" is the "default default".)
* **IApplicationBuilder.UseFileServer()** combines .UseDefaultFiles() and .UseStaticFiles().

### Configuring MVC
* Call **IApplicationBuilder.UseMvcWithDefaultRoute()** from [Startup.Configure](DotNetCoreFood/Startup.cs)
and **IServiceCollection.AddMvc()** in **Startup.ConfigureServices** to setup default ASP.NET MVC routing.
* By default, this looks for a *prefix*Controller file in the "Controllers" folder (default prefix is "Home") and
a public method (default is "Index") to execute, so "/", "/Home" and "/Home/Index" all call the same controller action.
-----

### Routing

**Convention-based routing: (routeBuilder.MapRoute)**

app.[UseMvcWithDefaultRoute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.mvcapplicationbuilderextensions.usemvcwithdefaultroute?view=aspnetcore-2.0)() in [Startup.Configure](DotNetCoreFood/Startup.cs) uses the template `{controller=Home}/{action=Index}/{id?}`

**Attribute-based routing:**

`[Route("[controller]/[action]")]` attributes applied to controller / controller action method

### Action Results
Controller actions return something that implements [IActionResult](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.iactionresult?view=aspnetcore-2.0)

Action results are "lazy". They don't generate content until executed via .ExecuteResultAsync(*context*)

**ObjectResult** tells the framework to use [content negotiation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/formatting#content-negotiation)

### Tag Helpers
* Add New Item "MVC View Imports Page" with directive `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers` (see [_ViewImports.cshtml](DotNetCoreFood/Views/_ViewImports.cshtml))

This is also the place to define other common namespaces (e.g. `@using MyProject.Models`)

https://www.exceptionnotfound.net/the-viewimports-cshtml-file-setting-up-view-namespaces-in-mvc-6/

### R4MVC

To generate code, from the Package Manager console, type `Generate-R4MVC`

-----

#### New-ish C# features I haven't used yet and want to try with this repo
✅ [expression-bodied method](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members) - [Program.BuildWebHost](DotNetCoreFood/Program.cs)

⬜️ expression-bodied property

⬜️ expression-bodied constructor

⬜️ [inline-declared out variable](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#out-variables)

⬜️ [tuple (multiple values) return from function](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#tuples)

⬜️ ["discards"](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#discards)

⬜️ [read-only auto-property](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#read-only-auto-properties)

⬜️ [read-only auto-property](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#read-only-auto-properties)

⬜️ [auto-property initializer](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#auto-property-initializers)

⬜️ [using static](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#using-static)

⬜️ ["Elvis operator"](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6#auto-property-initializers)

-----
#### Other TODOs
⬜️ add unit tests

✅ Make nuget packages from https://github.com/BrianSchroer/sparky-test-helpers & use in this repo