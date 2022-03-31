using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace marking_api.API
{
    /// <summary>
    /// Initial class run on system startup
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that is called initially on startup. Builds the application
        /// </summary>
        /// <param name="args">Startup arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Build application. Uses startup.cs to configure the application
        /// </summary>
        /// <param name="args">Startup arguments</param>
        /// <returns>Initialised application</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
