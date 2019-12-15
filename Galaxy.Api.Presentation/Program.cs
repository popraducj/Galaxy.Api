using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Galaxy.Api.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logConfig = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ENVIRONMENT"))
                ? "NLog.Development.config"
                : "NLog.config";
            var logger = NLogBuilder.ConfigureNLog(logConfig).GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5002", "https://*:5003")
                        .UseStartup<Startup>();
                })
                .ConfigureLogging((context, builder) => { builder.ClearProviders(); })
                .UseNLog();
    }
}
