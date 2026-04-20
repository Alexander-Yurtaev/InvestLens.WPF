using CsvHelper;
using CsvHelper.Configuration;
using InvestLens.Model.Crud.Transaction;
using System.Globalization;
using System.IO;

namespace InvestLens.ViewModel.Helpers;

public static class TransactionHelper
{
    public static List<TransactionModel> Convert(TextReader textReader)
    {
        var config = new CsvConfiguration(new CultureInfo("ru-RU"))
        {
            Delimiter = ","
        };

        using var csv = new CsvReader(textReader, config);
        csv.Context.RegisterClassMap<TransactionMap>();
        var transations = csv.GetRecords<TransactionModel>().ToList();
        return transations;
    }
}