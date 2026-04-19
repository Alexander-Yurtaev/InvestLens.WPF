using CsvHelper;
using InvestLens.Model.Crud.Transaction;
using System.Globalization;

namespace InvestLens.ViewModel.Helpers;

public static class TransactionHelper
{
    public static List<TransactionModel> Convert(TextReader streamReader)
    {
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<TransactionMap>();
        var transations = csv.GetRecords<TransactionModel>().ToList();
        return transations;
    }
}