namespace LagoaTrading.Shared.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal TruncateCurrency(this decimal value)
        => value.TruncateDecimal(2);

        public static decimal TruncateCrypto(this decimal value)
        => value.TruncateDecimal(8);

        public static decimal TruncateDecimal(this decimal value, int decimalPlaces)
        => decimal.Truncate(value * (decimal)Math.Pow(10, decimalPlaces)) / (decimal)Math.Pow(10, decimalPlaces);
    }
}
