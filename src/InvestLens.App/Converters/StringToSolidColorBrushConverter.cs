using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace InvestLens.App.Converters;

public class StringToSolidColorBrushConverter : IValueConverter
{
    private static StringToSolidColorBrushConverter? _instance;

    public static StringToSolidColorBrushConverter Instance
    {
        get
        {
            _instance ??= new StringToSolidColorBrushConverter();
            return _instance;
        }
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            var valueColorHex = value?.ToString() ?? "#00000000";
            var defaultColor = (Color)ColorConverter.ConvertFromString(valueColorHex);
            return new SolidColorBrush(defaultColor);
        }
        catch
        {
            return new SolidColorBrush(Colors.Transparent);
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}