namespace Simple_ETL_Project.Models;

public class CsvTripDataModel
{
    public int VendorID { get; set; }
    public DateTime tpep_pickup_datetime { get; set; }
    public DateTime tpep_dropoff_datetime { get; set; }
    public int passenger_count { get; set; }
    public double trip_distance { get; set; }
    public int RatecodeID { get; set; }
    public string store_and_fwd_flag { get; set; }
    public int PULocationID { get; set; }
    public int DOLocationID { get; set; }
    public string payment_type { get; set; }
    public decimal fare_amount { get; set; }
    public decimal extra { get; set; }
    public decimal mta_tax { get; set; }
    public decimal tip_amount { get; set; }
    public decimal tolls_amount { get; set; }
    public decimal improvement_surcharge { get; set; }
    public decimal total_amount { get; set; }
    public decimal congestion_surcharge { get; set; }
}