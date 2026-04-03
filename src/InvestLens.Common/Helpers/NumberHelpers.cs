namespace InvestLens.Common.Helpers;

public static class NumberHelpers
{
    public static bool IsInt(double value)
    {
        return Math.Abs(value - Math.Round(value)) < double.Epsilon;
    }
}