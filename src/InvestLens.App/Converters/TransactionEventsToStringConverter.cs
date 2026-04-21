using InvestLens.Model.Enums;
using System.Globalization;
using System.Windows.Data;

namespace InvestLens.App.Converters;

public class TransactionEventsToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return "ОШИБКА";
        if (Enum.TryParse<TransactionEvents>(value.ToString(), true, out var transactionEvent))
        {
            return transactionEvent switch
            {
                TransactionEvents.Buy => "Покупка",
                TransactionEvents.Sell => "Продажа",
                TransactionEvents.Dividend => "Дивиденд или купон",
                TransactionEvents.Stock_As_Dividend => "Дивиденд в виде акции",
                TransactionEvents.Split => "Сплит",
                TransactionEvents.Spinoff => "Спинофф",
                TransactionEvents.Fee => "Комиссия",
                TransactionEvents.Amortisation => "Амортизация по облигации",
                TransactionEvents.Repayment => "Полное погашение облигации",
                TransactionEvents.Cash_In => "Пополнение счета",
                TransactionEvents.Cash_Out => "Вывод средств со счета",
                TransactionEvents.Cash_Gain => "Прочие доходы",
                TransactionEvents.Cash_Expense => "Прочие расходы",
                TransactionEvents.Cash_Convert => "Конвертация",
                TransactionEvents.Tax => "НДФЛ",
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