using System.Globalization;

namespace InvestLens.Common.Helpers;

public static class NumberHelpers
{
    public static string ConvertValueToString(double value)
    {
        return value.ToString(IsInt(value) ? "N0" : "F1", CultureInfo.InvariantCulture);
    }

    public static bool IsInt(double value)
    {
        return Math.Abs(value - Math.Round(value)) < double.Epsilon;
    }
}