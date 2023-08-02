namespace LagoaTrading.Domain.Core.Tools
{
    public class LagoaTradingHelper
    {
        public static DateTime GetDateTimeFromUTC(double datetime)
        => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(datetime);
    }
}
