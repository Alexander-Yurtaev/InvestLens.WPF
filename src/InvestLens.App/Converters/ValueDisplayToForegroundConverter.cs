using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace InvestLens.App.Converters;

public class ValueDisplayToForegroundConverter : IValueConverter
{
    private static ValueDisplayToForegroundConverter? _instance;
    
    public static ValueDisplayToForegroundConverter Instance
    {
        get
        {
            _instance ??= new ValueDisplayToForegroundConverter();
            return _instance;
        }
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var valueDisplay = value?.ToString() ?? "";
        if (valueDisplay.StartsWith("-")) return new SolidColorBrush(Color.FromRgb(0xC8, 0x10, 0x2E));
        if (valueDisplay.StartsWith("+")) return new SolidColorBrush(Color.FromRgb(0x2C, 0x8C, 0x6E));

        try
        {
            var defaultColorHex = parameter?.ToString() ?? "#002855";
            var defaultColor = (Color)ColorConverter.ConvertFromString(defaultColorHex);
            return new SolidColorBrush(defaultColor);
        }
        catch
        {
            return new SolidColorBrush(Color.FromRgb(0x00, 0x28, 0x55));
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}