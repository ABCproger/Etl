namespace Simple_ETL_Project.Services.CLIManagement.MenuManagement;

public interface IMenuManagementService
{
    Task ImportDataFromFile(string filePath);
    Task GetHighestAverageTips();
    Task GetLongestTripsByDistance();
    Task GetLongestTripsByTime();
    Task SearchByPULocationId(int locationId);
}