using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog; //Serilog https://serilog.net/

namespace Sample.Api
{
    public class Program
    {
        ////// Load Settings from appsettings.json //////
        public static IConfiguration _configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            ////// Serilog //////
            Log.Logger = new LoggerConfiguration()
             .ReadFrom.Configuration(_configuration)                        
             .CreateLogger(); //Serilog
            ////// Serilog //////

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() //Serilog
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseSerilog(); //Serilog
                    webBuilder.UseStartup<Startup>();                    
                });        
    }
}
