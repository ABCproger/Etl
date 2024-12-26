namespace Simple_ETL_Project.Services.CLIManagementService;

public class CLIManagementService : ICLIManagementService
{
    
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

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //await ImportData();
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
                Console.WriteLine("1. Highest Average Tip by Location");
                Console.WriteLine("2. Top 100 Longest Trips by Distance");
                Console.WriteLine("3. Top 100 Longest Trips by Time");
                Console.WriteLine("4. Search by PULocationId");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("\nSelect option (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ShowHighestAverageTip();
                        break;
                    case "2":
                        await ShowLongestTripsByDistance();
                        break;
                    case "3":
                        await ShowLongestTripsByTime();
                        break;
                    case "4":
                        await SearchByLocation();
                        break;
                    case "5":
                        returnToMain = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ImportData()
        {
            Console.Clear();
            Console.WriteLine("=== Import Transaction Data ===");
            Console.Write("Enter file path: ");
            string filePath = Console.ReadLine();

            try
            {
                //TODO:
                //await _dataService.ImportDataFromFile(filePath);
                Console.WriteLine("Data import successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing data: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private async Task ShowHighestAverageTip()
        {
            try
            {
                //TODO:
                //using var reader = await _dataService.GetHighestAverageTips();
                Console.Clear();
                Console.WriteLine("=== Highest Average Tips by Location ===");
                Console.WriteLine("LocationID | Average Tip");
                Console.WriteLine("------------------------");

                // while (await reader.ReadAsync())
                // {
                //     Console.WriteLine($"{reader["PULocationId"],-10} | ${reader["AverageTip"]:F2}");
                // }
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
                // using var reader = await _dataService.GetLongestTripsByDistance();
                Console.Clear();
                Console.WriteLine("=== Top 100 Longest Trips by Distance ===");
                Console.WriteLine("From | To | Distance (miles) | Fare");
                Console.WriteLine("-----------------------------------");

                // while (await reader.ReadAsync())
                // {
                //     Console.WriteLine($"{reader["PULocationId"],-4} | {reader["DOLocationId"],-4} | {reader["trip_distance"],-14:F2} | ${reader["fare_amount"]:F2}");
                // }
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
                //using var reader = await _dataService.GetLongestTripsByTime();
                Console.Clear();
                Console.WriteLine("=== Top 100 Longest Trips by Time ===");
                Console.WriteLine("From | To | Duration (min) | Fare");
                Console.WriteLine("-----------------------------------");

                // while (await reader.ReadAsync())
                // {
                //     Console.WriteLine($"{reader["PULocationId"],-4} | {reader["DOLocationId"],-4} | {reader["duration_minutes"],-14} | ${reader["fare_amount"]:F2}");
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private async Task SearchByLocation()
        {
            Console.Clear();
            Console.WriteLine("=== Search by Pick-up Location ===");
            Console.Write("Enter PULocationId: ");
            
            if (int.TryParse(Console.ReadLine(), out int locationId))
            {
                try
                {
                    //using var reader = await _dataService.SearchByPULocationId(locationId);
                    Console.WriteLine("\nTrips from this location:");
                    Console.WriteLine("Pickup Time | Distance | Fare | Tip");
                    Console.WriteLine("-----------------------------------");

                    // while (await reader.ReadAsync())
                    // {
                    //     Console.WriteLine($"{reader["pickup_datetime"],-12:MM/dd HH:mm} | {reader["trip_distance"],-8:F1} | ${reader["fare_amount"],-5:F2} | ${reader["tip_amount"]:F2}");
                    // }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid location ID!");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

}