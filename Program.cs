using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SalesProductApi.DB;

namespace SalesProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(4999, listenOptions =>  listenOptions.Protocols = HttpProtocols.Http1);
                        options.ListenAnyIP(9090, listenOptions =>  listenOptions.Protocols = HttpProtocols.Http2);
                    })
                    .UseStartup<Startup>());
                // .ConfigureWebHostDefaults(webBuilder =>
                // {
                //     webBuilder.UseStartup<Startup>();
                // });

        private static void CreateDbIfNotExists(IHost host)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<ProductContext>();
                        context.Database.EnsureCreated();
                        // DbInitializer.Initialize(context);
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred creating the DB.");
                    }
                }
            }



    }
}
