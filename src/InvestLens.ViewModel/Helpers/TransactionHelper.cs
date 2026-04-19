using CsvHelper;
using CsvHelper.Configuration;
using InvestLens.Model.Crud.Transaction;
using System.Globalization;

namespace InvestLens.ViewModel.Helpers;

public static class TransactionHelper
{
    public static List<TransactionModel> Convert(TextReader streamReader)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);

        using var csv = new CsvReader(streamReader, config);
        csv.Context.RegisterClassMap<TransactionMap>();
        var transations = csv.GetRecords<TransactionModel>().ToList();
        return transations;
    }
}