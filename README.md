# DotNetCoreFood
ASP.NET Core food web app

Following along with [K Scott Allen's **ASP.NET Core Fundamentals** Pluralsight course](https://www.pluralsight.com/courses/aspdotnet-core-fundamentals)...

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
* The default implementation of [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=aspnetcore-2.0) 
is [Microsoft.Extensions.Configuration.ConfigurationRoot](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationroot?view=aspnetcore-2.0).
Its `Item[String}` property (e.g. `_configuration["Greeting"]`) looks at these sources (in order):
	* command line argument
    * environment variable
    * user secret
    * [**appsettings.json**](DotNetCoreFood/appsettings.json)

## New-ish C# features I haven't used yet and want to try with this repo
✅ expression-bodied method - [Program.BuildWebHost](DotNetCoreFood/Program.cs)

⬜️ expression-bodied property

⬜️ expression-bodied constructor

⬜️ [inline-declared out variable](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#out-variables)

⬜️ tuple with named fields

⬜️ tuple with named fields
