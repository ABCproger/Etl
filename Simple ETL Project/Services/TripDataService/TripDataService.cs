namespace Simple_ETL_Project.Services.TripDataService;

using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

public class TripDataService : ITripDataService
{
    private readonly BaseDbContext _dbContext;
    public TripDataService(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int?> GetPuLocationWithHighestAverageTipAsync()
    {
        var result = await _dbContext.TripDatas
            .GroupBy(t => t.PuLocationId)
            .Select(g => new
            {
                PULocationID = g.Key,
                AverageTipAmount = g.Average(t => t.TipAmount)
            })
            .OrderByDescending(x => x.AverageTipAmount)
            .FirstOrDefaultAsync();

        return result?.PULocationID;
    }

    public async Task<List<TripData>> GetTopLongestTripsByDistanceAsync()
    {
        return await _dbContext.TripDatas
            .OrderByDescending(t => t.TripDistance)
            .Take(100)
            .ToListAsync();
    }

    public async Task<List<TripData>> GetTopLongestTripsByTimeAsync()
    {
        return await _dbContext.TripDatas
            .OrderByDescending(t => EF.Functions.DateDiffSecond(t.TrepPickUpDateTime, t.TrepDropOffDateTime))
            .Take(100)
            .ToListAsync();
    }
}