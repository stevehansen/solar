using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Solar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);

                    config.AddEnvironmentVariables();

                    if (env.IsDevelopment())
                        config.AddUserSecrets<Program>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
