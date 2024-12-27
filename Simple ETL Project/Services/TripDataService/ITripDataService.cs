namespace Simple_ETL_Project.Services.TripDataService;

using Database.Entities;

public interface ITripDataService
{
    Task<int?> GetPuLocationWithHighestAverageTipAsync();
    Task<List<TripData>> GetTopLongestTripsByDistanceAsync();
    Task<List<TripData>> GetTopLongestTripsByTimeAsync();
}