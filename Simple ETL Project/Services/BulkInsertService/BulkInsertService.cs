namespace Simple_ETL_Project.Services.BulkInsertService;

using System.Data;
using System.Reflection;
using Database;
using Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class BulkInsertService : IBulkInsertService
{
    private readonly string? _connectionString;
    private readonly BaseDbContext _dbContext;

    public BulkInsertService(IConfiguration configuration, BaseDbContext dbContext)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _dbContext = dbContext;
    }

    public async Task BulkInsertAsync<T>(IEnumerable<T> records, int batchSize = 5000)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var bulkCopy = new SqlBulkCopy(connection);
        
        var tableName = GetTableNameFromContext<T>(_dbContext);
        bulkCopy.DestinationTableName = tableName;
        bulkCopy.BatchSize = batchSize;

        var table = CreateDataTable(records, _dbContext);
        
        foreach (DataColumn column in table.Columns)
        {
            var destinationColumn = GetColumnNameFromContext<T>(_dbContext, column.ColumnName);
            bulkCopy.ColumnMappings.Add(column.ColumnName, destinationColumn);
        }

        await bulkCopy.WriteToServerAsync(table);
    }

    private static string GetTableNameFromContext<T>(BaseDbContext context)
    {
        var entityType = context.Model.FindEntityType(typeof(T));
        var schema = entityType.GetSchema();
        var tableName = entityType.GetTableName();

        return string.IsNullOrEmpty(schema) ? tableName : $"{schema}.{tableName}";
    }

    private static string GetColumnNameFromContext<T>(BaseDbContext context, string propertyName)
    {
        var entityType = context.Model.FindEntityType(typeof(T));
        var property = entityType?.FindProperty(propertyName);
        return property?.GetColumnName() ?? propertyName.ToSnakeCase();
    }

    private static DataTable CreateDataTable<T>(
        IEnumerable<T> records,
        BaseDbContext context)
    {
        var table = new DataTable();
        var entityType = context.Model.FindEntityType(typeof(T));
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        foreach (var property in properties)
        {
            var efProperty = entityType?.FindProperty(property.Name);
            
            if (efProperty == null)
                continue;

            var columnType = GetColumnType(efProperty.ClrType);
            table.Columns.Add(property.Name, columnType);
        }
        
        foreach (var record in records)
        {
            var row = table.NewRow();
            foreach (var property in properties)
            {
                if (!table.Columns.Contains(property.Name))
                    continue;

                var value = property.GetValue(record);
                row[property.Name] = value ?? DBNull.Value;
            }

            table.Rows.Add(row);
        }

        return table;
    }

    private static Type GetColumnType(Type propertyType)
    {
        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            propertyType = propertyType.GetGenericArguments()[0];
        }
        
        if (propertyType.IsEnum)
        {
            return typeof(int);
        }

        return propertyType;
    }
}