using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using SpillTracker.Utilities;
using SpillTracker.Data;
using SpillTracker.Utilities;

namespace SpillTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();//default line
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    // Get the Iconfiguration service that allows us to query user-secrets and the configuration on Azure
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    // Set password with the Secret Manager tool or store in Azure app configuration
                    // dotnet user-secrets set SeedUserPW <pw>

                    var testUserPW = config["SeedUserPW"];
                    var adminPW = config["SeedAdminPW"];

                    SeedUsers.Initialize(services, SeedData.UserSeedData, testUserPW).Wait();
                    SeedUsers.InitializeAdmin(services, "admin@example.com", "admin", adminPW, "The", "Admin").Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured seeding users in the Identity DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
