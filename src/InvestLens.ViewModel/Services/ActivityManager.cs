using InvestLens.Model;
using InvestLens.Model.Entities;
using InvestLens.Model.Helpers;

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
            .Select(t => new ActivityItem("🟢", t))
            .ToList();

        //var result = new List<ActivityItem>
        //{
        //    new ActivityItem("🟢", "Покупка AAPL", "+125 акций", "$21,875", "2 дня назад"),
        //    new ActivityItem("🔴", "Продажа TSLA", "-50 акций", "$8,250", "5 дней назад"),
        //    new ActivityItem("💵", "Дивиденды MSFT", "Квартальные", "$420", "12 дней назад"),
        //};

        return result;
    }
}
