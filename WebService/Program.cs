using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace WebService
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                                                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                                  .Enrich.FromLogContext()
                                                  .WriteTo.Console()
                                                  .CreateLogger();

            try
            {
                Log.Information("Iniciando Integración Esmeralda");
                CreateHostBuilder(args)
                   .Build()
                   .Run();

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
