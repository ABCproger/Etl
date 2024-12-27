namespace Simple_ETL_Project.Services.TripDataService;

using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class TripDataService : ITripDataService
{
    private readonly BaseDbContext _dbContext;
    private readonly ILogger<TripDataService> _logger;
    public TripDataService(BaseDbContext dbContext, ILogger<TripDataService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<int?> GetPuLocationWithHighestAverageTipAsync()
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving PULocation with the highest average tip: {ex.Message}", ex);
            throw;
        }
    }

    public async Task<List<TripData>> GetTopLongestTripsByDistanceAsync()
    {
        try
        {
            return await _dbContext.TripDatas
                .OrderByDescending(t => t.TripDistance)
                .Take(100)
                .ToListAsync(); 
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving longest trips by distance: {ex.Message}", ex);
            throw;
        }
    }

    public async Task<List<TripData>> GetTopLongestTripsByTimeAsync()
    {
        try
        {
            return await _dbContext.TripDatas
                .OrderByDescending(t => EF.Functions.DateDiffSecond(t.TrepPickUpDateTime, t.TrepDropOffDateTime))
                .Take(100)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving longest trips by distance: {ex.Message}", ex);
            throw;
        }
    }
}