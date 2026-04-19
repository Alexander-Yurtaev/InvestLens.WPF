using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace InvestLens.ViewModel.Helpers.TypeConverters;

public class CustomDateTimeConverter : ITypeConverter
{
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text)) return null;
        return DateTime.ParseExact(text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        throw new NotImplementedException();
    }
}