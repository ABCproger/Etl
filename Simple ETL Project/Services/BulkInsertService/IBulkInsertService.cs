namespace Simple_ETL_Project.Services.BulkInsertService;

public interface IBulkInsertService
{
    Task BulkInsertAsync<T>(IEnumerable<T> records, int batchSize = 5000);
}