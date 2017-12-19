using JetBrains.Annotations;
using Microsoft.Z3;

namespace ZedZedZed.Extensions
{
    public static class RatNumExtensions
    {
        public static decimal AsDecimal([NotNull] this RatNum number)
        {
            var numerator = (decimal)number.BigIntNumerator;
            var denominator = (decimal)number.BigIntDenominator;

            return numerator / denominator;
        }
    }
}
