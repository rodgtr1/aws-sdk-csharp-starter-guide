using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using app.Models;

namespace app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AwsOptions>(hostContext.Configuration.GetSection("AWS"));
                    services.AddHttpClient();
                    services.AddHostedService<Worker>();
                });
    }
}
