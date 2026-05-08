using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvestLens.Model.Enums;

public enum TransactionEvent
{
    /// <summary>
    /// Покупка
    /// </summary>
    Buy,
    /// <summary>
    /// Продажа
    /// </summary>
    Sell,
    /// <summary>
    /// Дивиденд или купон
    /// </summary>
    Dividend,
    /// <summary>
    /// Дивиденд в виде акции
    /// </summary>
    Stock_As_Dividend,
    /// <summary>
    /// Сплит
    /// </summary>
    Split,
    /// <summary>
    /// Спинофф
    /// </summary>
    Spinoff,
    /// <summary>
    /// Комиссия
    /// </summary>
    Fee,
    /// <summary>
    /// Амортизация по облигации
    /// </summary>
    Amortisation,
    /// <summary>
    /// Полное погашение облигации
    /// </summary>
    Repayment,
    /// <summary>
    /// Пополнение счета
    /// </summary>
    Cash_In,
    /// <summary>
    /// Вывод средств со счета
    /// </summary>
    Cash_Out,
    /// <summary>
    /// Прочие доходы в валюте (например, возврат налогов)
    /// </summary>
    Cash_Gain,
    /// <summary>
    /// Прочие расходы в валюте (например, налоги на прирост капитала)
    /// </summary>
    Cash_Expense,
    /// <summary>
    /// Конвертация, или покупка и продажа валюты, см. пример такой записи внизу
    /// </summary>
    Cash_Convert,
    /// <summary>
    /// Погашение обязательств по НДФЛ
    /// </summary>
    Tax
}