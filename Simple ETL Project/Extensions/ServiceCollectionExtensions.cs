namespace Simple_ETL_Project.Extensions;

using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.BulkInsertService;
using Services.CLIManagementService;
using Services.CsvProcessorService;
using Services.TripDataService;
using Simple_ETL_Project.Configurations.CsvConfigurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging();
        services.AddTransient<ICLIManagementService, CLIManagementService>();
        services.AddScoped<IBulkInsertService, BulkInsertService>();
        services.AddTransient<ICsvProcessorService, CsvProcessorService>();
        services.AddTransient<ITripDataService, TripDataService>();
        
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        var csvOptions = new CsvOptions();
        configuration.Bind(nameof(CsvOptions), csvOptions);
    
        return services;
    }
}