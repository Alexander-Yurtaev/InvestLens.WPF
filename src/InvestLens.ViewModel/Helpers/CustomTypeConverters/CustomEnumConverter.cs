using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Helpers.TypeConverters;

public class CustomEnumConverter : ITypeConverter
{
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return Enum.Parse<TransactionEvents>(text, true);
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        throw new NotImplementedException();
    }
}