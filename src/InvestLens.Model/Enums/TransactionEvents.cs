using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvestLens.Model.Enums;

public enum TransactionEvents
{
    // Покупка
    Buy,
    // Продажа
    Sell,
    // Дивиденд или купон
    Dividend,
    // Дивиденд в виде акции
    Stock_As_Dividend,
    // Сплит
    Split,
    // Спинофф
    Spinoff,
    // Комиссия
    Fee,
    // Амортизация по облигации
    Amortisation,
    // Полное погашение облигации
    Repayment,
    // Пополнение счета
    Cash_In,
    // Вывод средств со счета
    Cash_Out,
    // Прочие доходы в валюте (например, возврат налогов)
    Cash_Gain,
    // Прочие расходы в валюте (например, налоги на прирост капитала)
    Cash_Expense,
    // Конвертация, или покупка и продажа валюты, см. пример такой записи внизу
    Cash_Convert
}