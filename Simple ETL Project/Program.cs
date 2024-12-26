namespace Simple_ETL_Project;

using System.IO;
using System.Threading.Tasks;
using Database;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.CLIManagementService;

public class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .Build();

        var serviceCollection = new ServiceCollection()
            .SetupServices(configuration);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var dbContext = serviceProvider.GetRequiredService<BaseDbContext>();
        
        await dbContext.Database.MigrateAsync();
        
        var cliManagementService = serviceProvider.GetRequiredService<ICLIManagementService>(); 
        
        await cliManagementService.RunAsync();
    }
}
