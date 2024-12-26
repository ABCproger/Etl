namespace Simple_ETL_Project;

using System.IO;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
