using InvestLens.Model;
using InvestLens.Model.Entities;

namespace InvestLens.ViewModel.Services;

public class ActivityManager : IActivityManager
{
    private readonly IPortfoliosManager _portfoliosManager;

    public ActivityManager(IPortfoliosManager portfoliosManager)
    {
        _portfoliosManager = portfoliosManager;
    }

    public async Task<List<ActivityItem>> GetActivityItems()
    {
        var transactions = await _portfoliosManager.GetLastTtransactions(3);

        var result = transactions
                                       // icon,           title,                       description,                 amount,             date
            .Select(t => new ActivityItem("🟢", $"{t.Event.ToString()} {t.Symbol}", GetDescription(t), GetTotalCost(t).ToString("C2"), t.Date.Date.ToString("dd-MM-yyyy")))
            .ToList();

        //var result = new List<ActivityItem>
        //{
        //    new ActivityItem("🟢", "Покупка AAPL", "+125 акций", "$21,875", "2 дня назад"),
        //    new ActivityItem("🔴", "Продажа TSLA", "-50 акций", "$8,250", "5 дней назад"),
        //    new ActivityItem("💵", "Дивиденды MSFT", "Квартальные", "$420", "12 дней назад"),
        //};

        return result;
    }

    private string GetDescription(Transaction transaction)
    {
        if (transaction.Event == Model.Enums.TransactionEvent.Buy)
        {
            return $"+{transaction.Quantity.ToString("N2")} шт";
        }

        if (transaction.Event == Model.Enums.TransactionEvent.Sell)
        {
            return $"-{transaction.Quantity.ToString("N2")} шт";
        }

        return transaction.Portfolio?.Name ?? "-";
    }

    private decimal GetTotalCost(Transaction transaction)
    {
        return transaction.Price * transaction.Quantity + transaction.FeeTax;
    }
}
