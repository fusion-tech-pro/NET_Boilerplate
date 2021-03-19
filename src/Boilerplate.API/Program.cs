namespace Boilerplate.API
{
    #region << Using >>

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                                                 {
                                                     webBuilder.UseStartup<Startup>();
                                                 })
                       .UseSerilog((hostingContext, services, loggerConfiguration) =>
                                   {
                                       loggerConfiguration
                                               .ReadFrom.Configuration(hostingContext.Configuration);
                                   }, writeToProviders: true);
        }
    }
}