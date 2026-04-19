using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InvestLens.App.Converters;

public class InverseBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var boolValue = value is bool v ? v : false;
        return boolValue ? Visibility.Hidden : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var visibleValue = value is Visibility v ? v : Visibility.Hidden;
        return visibleValue = Visibility.Hidden;
    }
}