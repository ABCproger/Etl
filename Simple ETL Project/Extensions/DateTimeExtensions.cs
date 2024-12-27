namespace Simple_ETL_Project.Extensions;

public static class DateTimeExtensions
{
    private static readonly TimeZoneInfo EstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
    
    public static DateTime ConvertToUtcFromEst(this DateTime estDateTime)
    {
        var estDateTimeWithZone = TimeZoneInfo.ConvertTimeToUtc(estDateTime, EstTimeZone);
        return estDateTimeWithZone;
    }
}