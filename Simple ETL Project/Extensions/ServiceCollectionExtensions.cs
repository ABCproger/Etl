namespace Simple_ETL_Project.Extensions;

using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.BulkInsertService;
using Services.CLIManagementService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICLIManagementService, CLIManagementService>();
        services.AddTransient<IBulkInsertService, BulkInsertService>();
        
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    
        return services;
    }
}