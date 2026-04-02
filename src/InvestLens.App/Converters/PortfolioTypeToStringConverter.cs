using System.Globalization;
using System.Windows.Data;
using InvestLens.Model.Enums;

namespace InvestLens.App.Converters;

public class PortfolioTypeToStringConverter : IValueConverter
{
    private static PortfolioTypeToStringConverter? _instance;

    public static PortfolioTypeToStringConverter Instance
    {
        get
        {
            _instance ??= new PortfolioTypeToStringConverter();
            return _instance;
        }
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var portfolioType = value is PortfolioType type ? type : PortfolioType.Primary;
        return portfolioType switch
        {
            PortfolioType.Primary => "Основной",
            PortfolioType.Agressive => "Агрессивный",
            PortfolioType.Dividend => "Дивидендный",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}