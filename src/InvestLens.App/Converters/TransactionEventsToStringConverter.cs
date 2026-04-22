using InvestLens.Model.Enums;
using System.Globalization;
using System.Windows.Data;

namespace InvestLens.App.Converters;

public class TransactionEventsToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return "ОШИБКА";
        if (Enum.TryParse<TransactionEvent>(value.ToString(), true, out var transactionEvent))
        {
            return transactionEvent switch
            {
                TransactionEvent.Buy => "Покупка",
                TransactionEvent.Sell => "Продажа",
                TransactionEvent.Dividend => "Дивиденд или купон",
                TransactionEvent.Stock_As_Dividend => "Дивиденд в виде акции",
                TransactionEvent.Split => "Сплит",
                TransactionEvent.Spinoff => "Спинофф",
                TransactionEvent.Fee => "Комиссия",
                TransactionEvent.Amortisation => "Амортизация по облигации",
                TransactionEvent.Repayment => "Полное погашение облигации",
                TransactionEvent.Cash_In => "Пополнение счета",
                TransactionEvent.Cash_Out => "Вывод средств со счета",
                TransactionEvent.Cash_Gain => "Прочие доходы",
                TransactionEvent.Cash_Expense => "Прочие расходы",
                TransactionEvent.Cash_Convert => "Конвертация",
                TransactionEvent.Tax => "НДФЛ",
                _ => "ОШИБКА"
            };
        }
        return "ОШИБКА";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}