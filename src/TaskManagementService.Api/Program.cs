using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace TaskManagementService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //При использовании конфигов из appsettings, почему-то не сплитило логи по левелу
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Logger(c =>
                    c.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                        .WriteTo.File("../../Log/Info.txt",
                            outputTemplate:
                            "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}"))
                .WriteTo.Logger(c =>
                    c.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                        .WriteTo.File("../../Log/Error.txt",
                            outputTemplate:
                            "{Timestamp:o} [{Level:u3}] ({SourceContext}) {Message}{NewLine}{Exception}"))
                .CreateLogger();

            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(hostConfig => { hostConfig.SetBasePath(Directory.GetCurrentDirectory()); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog();
        
    }
}