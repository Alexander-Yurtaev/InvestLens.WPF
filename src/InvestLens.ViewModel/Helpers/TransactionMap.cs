using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using InvestLens.Model.Crud.Transaction;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.Helpers.TypeConverters;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Globalization;

namespace InvestLens.ViewModel.Helpers;

public class TransactionMap : ClassMap<TransactionModel>
{
    public TransactionMap()
    {
        Map(m => m.Event).Name("Event").TypeConverter<CustomEnumConverter>();
        Map(m => m.Date).Name("Date").TypeConverter<CustomDateTimeConverter>();
        Map(m => m.Symbol).Name("Symbol");
        Map(m => m.Price).Name("Price").TypeConverter<DecimalConverter>();
        Map(m => m.Quantity).Name("Quantity").TypeConverter<DecimalConverter>();
        Map(m => m.Currency).Name("Currency");
        Map(m => m.FeeTax).Name("FeeTax").TypeConverter<DecimalConverter>();
        Map(m => m.Exchange).Name("Exchange").Optional();
        Map(m => m.NKD).Name("NKD").Optional().TypeConverter<CustomNullableDecimalConverter>();
        Map(m => m.FeeCurrency).Name("FeeCurrency").Optional();
        Map(m => m.DoNotAdjustCash).Name("DoNotAdjustCash").Optional().TypeConverter<CustomNullableBoolConverter>();
        Map(m => m.Note).Name("Note").Optional();
    }
}