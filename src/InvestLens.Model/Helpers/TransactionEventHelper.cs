using InvestLens.Model.Enums;
using System.Drawing;

namespace InvestLens.Model.Helpers;

public static class TransactionEventHelper
{
    public static string EventToString(object value)
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

    public static string EventToColor(TransactionEvent value)
    {
        return value switch
        {
            TransactionEvent.Buy => "#2C8C6E",
            TransactionEvent.Sell => "#C8102E",
            TransactionEvent.Dividend => "#2C8C6E",
            TransactionEvent.Stock_As_Dividend => "#2C8C6E",
            TransactionEvent.Split => "#E9EDF2",
            TransactionEvent.Spinoff => "#E9EDF2",
            TransactionEvent.Fee => "#E6B84E",
            TransactionEvent.Amortisation => "#2C8C6E",
            TransactionEvent.Repayment => "#2C8C6E",
            TransactionEvent.Cash_In => "#2C8C6E",
            TransactionEvent.Cash_Out => "#C8102E",
            TransactionEvent.Cash_Gain => "#2C8C6E",
            TransactionEvent.Cash_Expense => "#2C8C6E",
            TransactionEvent.Cash_Convert => "#2C8C6E",
            TransactionEvent.Tax => "#E6B84E",
            _ => "#E9EDF2"
        };
    }
}