namespace Simple_ETL_Project.Services.CLIManagementService;

using CsvProcessorService;
using TripDataService;

public class CLIManagementService : ICLIManagementService
{
    private readonly ICsvProcessorService _csvProcessorService;
    private readonly ITripDataService _tripDataService;

    public CLIManagementService(ICsvProcessorService csvProcessorService, ITripDataService tripDataService)
    {
        _csvProcessorService = csvProcessorService;
        _tripDataService = tripDataService;
    }

    public async Task RunAsync()
    {
        bool exitApplication = false;
        while (!exitApplication)
        {
            exitApplication = await ShowMainMenu();
        }
    }

    private async Task<bool> ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Test task Management System ===");
            Console.WriteLine("1. Import Transaction Data");
            Console.WriteLine("2. User analytics Queries");
            Console.WriteLine("3. Exit");
            Console.Write("\nSelect option (1-3): ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _csvProcessorService.ProcessCsvAsync();
                    break;
                case "2":
                    await ShowAnalyticsMenu();
                    break;
                case "3":
                    return true;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task ShowAnalyticsMenu()
    {
        bool returnToMain = false;
        while (!returnToMain)
        {
            Console.Clear();
            Console.WriteLine("=== User analytics Queries ===");
            Console.WriteLine("1. Find out which PuLocationId has the highest tip amount on average");
            Console.WriteLine("2. Top 100 Longest Tares in terms of trip distance");
            Console.WriteLine("3. Top 100 Longest Tares in terms of time spent traveling");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write("\nSelect option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowPuLocationWithHighestAverageTipAsync();
                    break;
                case "2":
                    await ShowLongestTripsByDistance();
                    break;
                case "3":
                    await ShowLongestTripsByTime();
                    break;
                case "4":
                    returnToMain = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task ShowPuLocationWithHighestAverageTipAsync()
    {
        try
        {
            var result = await _tripDataService.GetPuLocationWithHighestAverageTipAsync();
            Console.WriteLine($"\n{result}");
            await ShowAnalyticsMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ShowLongestTripsByDistance()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Top 100 Longest Trips in terms of trip distance ===");
            Console.WriteLine("-----------------------------------");
            
            var result = await _tripDataService.GetTopLongestTripsByDistanceAsync();


            if (result.Count > 0)
            {
                foreach (var trip in result)
                {
                    Console.WriteLine($"Trip Distance: {trip.TripDistance} | " +
                                      $"PULocationID: {trip.PuLocationId} | " +
                                      $"DOLocationID: {trip.DoLocationId} | " +
                                      $"Fare Amount: {trip.FareAmount} | " +
                                      $"Tip Amount: {trip.TipAmount}");
                }
            }
            else
            {
                Console.WriteLine("No trips found.");
            }
            await ShowAnalyticsMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }


    private async Task ShowLongestTripsByTime()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Top 100 Longest Trips by Time ===");
            Console.WriteLine("-----------------------------------");
            
            var result = await _tripDataService.GetTopLongestTripsByTimeAsync();
            
            if (result.Count > 0)
            {
                foreach (var trip in result)
                {
                    Console.WriteLine($"PULocationID: {trip.PuLocationId} | " +
                                      $"DOLocationID: {trip.DoLocationId} | " +
                                      $"Trip Distance: {trip.TripDistance} | " +
                                      $"Fare Amount: {trip.FareAmount} | " +
                                      $"Tip Amount: {trip.TipAmount}");
                }
            }
            else
            {
                Console.WriteLine("No trips found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

}