using Microsoft.Z3;

namespace ZedZedZed.Extensions
{
    public static class RatNumExtensions
    {
        public static decimal AsDecimal(this RatNum number)
        {
            var numerator = (decimal)number.BigIntNumerator;
            var denominator = (decimal)number.BigIntDenominator;

            return numerator / denominator;
        }
    }
}
