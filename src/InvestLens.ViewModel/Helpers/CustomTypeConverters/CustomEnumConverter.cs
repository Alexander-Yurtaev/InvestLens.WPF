using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Helpers.TypeConverters;

public class CustomEnumConverter : ITypeConverter
{
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }
        return Enum.Parse<TransactionEvent>(text, true);
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        throw new NotImplementedException();
    }
}