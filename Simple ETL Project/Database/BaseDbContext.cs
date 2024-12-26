namespace Simple_ETL_Project.Database;

using Entities;
using Extensions;
using Microsoft.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {}

    public DbSet<TripData> TripDatas { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.OnDeleteRestrictRules();
        modelBuilder.AddDatabaseNamingRules();
    }
}