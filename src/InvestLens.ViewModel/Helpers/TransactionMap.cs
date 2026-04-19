using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using InvestLens.Model.Crud.Transaction;
using InvestLens.ViewModel.Helpers.TypeConverters;

namespace InvestLens.ViewModel.Helpers;

public class TransactionMap : ClassMap<TransactionModel>
{
    public TransactionMap()
    {
        Map(m => m.Event).Name("Event");
        Map(m => m.Date).Name("Date").TypeConverter<DateTymeConverter>();
        Map(m => m.Symbol).Name("Symbol");
        Map(m => m.Price).Name("Price").TypeConverter<DecimalConverter>();
        Map(m => m.Quantity).Name("Quantity").TypeConverter<DecimalConverter>();
        Map(m => m.Currency).Name("Currency");
        Map(m => m.FeeTax).Name("FeeTax").TypeConverter<DecimalConverter>();
        Map(m => m.Exchange).Name("Exchange").Optional();
        Map(m => m.NKD).Name("NKD").Optional().TypeConverter<NullableDecimalConverter>();
        Map(m => m.FeeCurrency).Name("FeeCurrency").Optional();
        Map(m => m.DoNotAdjustCash).Name("DoNotAdjustCash").Optional().TypeConverter<NullableBoolConverter>();
        Map(m => m.Note).Name("Note").Optional();
    }
}