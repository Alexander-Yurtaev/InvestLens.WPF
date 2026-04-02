using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using InvestLens.Model.Enums;

namespace InvestLens.App.Converters;

public class PortfolioTypeToForegroundConverter : IValueConverter
{
    private static PortfolioTypeToForegroundConverter? _instance;

    public static PortfolioTypeToForegroundConverter Instance
    {
        get
        {
            _instance ??= new PortfolioTypeToForegroundConverter();
            return _instance;
        }
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var portfolioType = value is PortfolioType type ? type : PortfolioType.Primary;
        return portfolioType switch
        {
            PortfolioType.Primary => new SolidColorBrush(Color.FromArgb(0xFF, 0xC8, 0x10, 0x2E)),
            PortfolioType.Agressive => new SolidColorBrush(Color.FromArgb(0xFF, 0x2C, 0x8C, 0x6E)),
            PortfolioType.Dividend => new SolidColorBrush(Color.FromArgb(0xFF, 0x2C, 0x8C, 0x6E)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}