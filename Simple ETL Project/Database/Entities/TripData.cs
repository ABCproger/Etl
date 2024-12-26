namespace Simple_ETL_Project.Database.Entities;

public class TripData
{
    public int Id { get; set; }
    public DateTime TrepPickUpDateTime { get; set; }
    public DateTime TrepDropOffDateTime { get; set; }
    public int PassengerCount { get; set; }
    public double TripDistance { get; set; }
    public string? StoreAndFwdFlag { get; set; }
    public int PuLocationId { get; set; }
    public int DoLocationId { get; set; }
    public decimal FareAmount { get; set; }
    public decimal TipAmount { get; set; }
}