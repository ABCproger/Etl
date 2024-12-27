namespace Simple_ETL_Project.Services.CsvProcessorService;

using System.Globalization;
using BulkInsertService;
using CsvHelper;
using CsvHelper.Configuration;
using Database.Entities;
using Extensions;
using Microsoft.Extensions.Logging;
using Models;

public class CsvProcessorService : ICsvProcessorService
{
    private readonly HashSet<string> _uniqueKeys = new();
    private readonly List<TripData> _batch = new();
    private readonly IBulkInsertService _bulkInsertService;
    private readonly ILogger<CsvProcessorService> _logger;

    public CsvProcessorService(IBulkInsertService bulkInsertService, ILogger<CsvProcessorService> logger)
    {
        _bulkInsertService = bulkInsertService;
        _logger = logger;
    }

    public async Task ProcessCsvAsync()
    {
        const string duplicatesFilePath = "duplicates.csv";
        try
        {
            using var reader = new StreamReader($"F:\\projects\\Simple ETL Project\\sample-cab-data.csv");
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true
            });

            await csvReader.ReadAsync();
            csvReader.ReadHeader();

            await using var duplicatesWriter = new StreamWriter(duplicatesFilePath);
            await using var csvDuplicatesWriter =
                new CsvWriter(duplicatesWriter, new CsvConfiguration(CultureInfo.InvariantCulture));

            csvDuplicatesWriter.WriteHeader<TripData>();
            await csvDuplicatesWriter.NextRecordAsync();

            while (await csvReader.ReadAsync())
            {
                var csvRecord = new CsvTripDataModel
                {
                    VendorID = csvReader.GetField<int>("VendorID"),
                    tpep_pickup_datetime = csvReader.GetField<DateTime>("tpep_pickup_datetime"),
                    tpep_dropoff_datetime = csvReader.GetField<DateTime>("tpep_dropoff_datetime"),
                    passenger_count = csvReader.GetField<int>("passenger_count"),
                    trip_distance = csvReader.GetField<double>("trip_distance"),
                    RatecodeID = csvReader.GetField<int>("RatecodeID"),
                    store_and_fwd_flag = csvReader.GetField<string>("store_and_fwd_flag"),
                    PULocationID = csvReader.GetField<int>("PULocationID"),
                    DOLocationID = csvReader.GetField<int>("DOLocationID"),
                    payment_type = csvReader.GetField<string>("payment_type"),
                    fare_amount = csvReader.GetField<decimal>("fare_amount"),
                    extra = csvReader.GetField<decimal>("extra"),
                    mta_tax = csvReader.GetField<decimal>("mta_tax"),
                    tip_amount = csvReader.GetField<decimal>("tip_amount"),
                    tolls_amount = csvReader.GetField<decimal>("tolls_amount"),
                    improvement_surcharge = csvReader.GetField<decimal>("improvement_surcharge"),
                    total_amount = csvReader.GetField<decimal>("total_amount"),
                    congestion_surcharge = csvReader.GetField<decimal>("congestion_surcharge")
                };

                var pickupDateTimeUtc = csvRecord.tpep_pickup_datetime.ConvertToUtcFromEst();
                var dropoffDateTimeUtc = csvRecord.tpep_dropoff_datetime.ConvertToUtcFromEst();

                var record = new TripData
                {
                    TrepPickUpDateTime = pickupDateTimeUtc,
                    TrepDropOffDateTime = dropoffDateTimeUtc,
                    PassengerCount = csvRecord.passenger_count,
                    TripDistance = csvRecord.trip_distance,
                    StoreAndFwdFlag = NormalizeFlag(csvRecord.store_and_fwd_flag),
                    PuLocationId = csvRecord.PULocationID,
                    DoLocationId = csvRecord.DOLocationID,
                    FareAmount = csvRecord.fare_amount,
                    TipAmount = csvRecord.tip_amount
                };

                var uniqueKey = GenerateUniqueKey(record);

                if (!_uniqueKeys.Add(uniqueKey))
                {
                    csvDuplicatesWriter.WriteRecord(record);
                    await csvDuplicatesWriter.NextRecordAsync();
                }
                else
                {
                    _batch.Add(record);

                    if (_batch.Count >= 5000)
                    {
                        await _bulkInsertService.BulkInsertAsync(_batch);
                        _batch.Clear();
                    }
                }
            }

            if (_batch.Count > 0)
            {
                await _bulkInsertService.BulkInsertAsync(_batch);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while processing the CSV file: {ex.Message}", ex);
        }
    }


    private static string GenerateUniqueKey(TripData record)
    {
        return $"{record.TrepPickUpDateTime:O}|{record.TrepDropOffDateTime:O}|{record.PassengerCount}";
    }

    private static string? NormalizeFlag(string? flag)
    {
        return flag?.Trim() switch
        {
            "N" => "No",
            "Y" => "Yes",
            _ => flag?.Trim()
        };
    }
}