namespace Simple_ETL_Project.Database.DbContextFactory;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class BaseDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    private readonly string? _connectionString;

    public BaseDbContextFactory() { }

    public BaseDbContextFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public BaseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
        
        optionsBuilder.UseSqlServer(_connectionString); 

        return new BaseDbContext(optionsBuilder.Options);
    }
}