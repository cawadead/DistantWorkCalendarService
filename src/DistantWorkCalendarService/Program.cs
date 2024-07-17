using DistantWorkCalendarService;
using DistantWorkCalendarService.Data;

public class Program
{
    public static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
            scope.ServiceProvider.GetRequiredService<DbSeed>().InitializeAsync().Wait();

        host.Run();
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