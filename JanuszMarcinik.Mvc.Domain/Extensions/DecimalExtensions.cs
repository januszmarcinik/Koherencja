namespace JanuszMarcinik.Mvc
{
    public static class DecimalExtensions
    {
        public static string TwoOrZeroDecimalPlaces(this decimal value)
        {
            if ((value % 1) == 0)
            {
                return value.ToString("N0");
            }
            else
            {
                return value.ToString("N2");
            }
        }

        public static decimal ToDecimal(this double value)
        {
            try
            {
                return (decimal)value;
            }
            catch
            {
                return 0;
            }
        }
    }
}