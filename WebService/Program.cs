using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace WebService
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var cb = new ConfigurationBuilder();
            var configuration = cb.SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json", true, true)
                                       .AddJsonFile(
                                            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                                            false,
                                            true
                                        )
                                       .AddCommandLine(args)
                                       .AddEnvironmentVariables()
                                       .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                                                  .WriteTo.Console(formatter:new JsonFormatter())
                                                  .CreateLogger();

            try
            {
                Log.Information("Iniciando API de integración Esmeralda");
                CreateHostBuilder(args)
                   .Build()
                   .Run();
                
                Log.Information("Parando API de integración ...");

                return 0;
            }
            catch (Exception e)
            {
                Log.Fatal(e,"Integración Esmeralda Terminó Inesperadamente");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseSerilog()
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
