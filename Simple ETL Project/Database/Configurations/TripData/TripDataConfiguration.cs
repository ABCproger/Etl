namespace Simple_ETL_Project.Database.Configurations.TripData;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TripDataConfiguration :IEntityTypeConfiguration<TripData>
{
    public void Configure(EntityTypeBuilder<TripData> builder)
    {
        builder.HasIndex(t => t.PuLocationId)
            .HasDatabaseName("IX_PULocationID");
        
        builder.HasIndex(t => t.TripDistance)
            .HasDatabaseName("IX_trip_distance");
        
        builder.HasIndex(t => new { t.TrepPickUpDateTime, t.TrepDropOffDateTime })
            .HasDatabaseName("IX_pickup_dropoff_datetime");
    }
}