using DistantWorkCalendarService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
                              .ConfigureAppConfiguration(config =>
                              { 
                                  config.SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                        .AddEnvironmentVariables()
                                        .AddCommandLine(args)
                                        .Build();
                              });

        return hostBuilder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        }); ;
    }
}