using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot_API_Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddOcelot();
                })
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseOcelot().Wait();                    
                })
                .Build()
                .Run();
        }
    }
}