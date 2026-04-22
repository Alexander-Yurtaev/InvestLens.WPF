using System.Globalization;

namespace InvestLens.Common.Helpers;

public static class NumberHelpers
{
    public static string ConvertValueToString(decimal value)
    {
        return value.ToString(IsInt(value) ? "N0" : "F1", CultureInfo.InvariantCulture);
    }

    public static string ConvertPersentValueToString(decimal value)
    {
        return value.ToString("P2", CultureInfo.InvariantCulture);
    }

    public static bool IsInt(decimal value)
    {
        return Math.Abs(value - Math.Round(value)) == 0;
    }
}