using InvestLens.Model.Helpers;
using System.Globalization;
using System.Windows.Data;

namespace InvestLens.App.Converters;

public class TransactionEventsToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return TransactionEventHelper.EventToString(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}