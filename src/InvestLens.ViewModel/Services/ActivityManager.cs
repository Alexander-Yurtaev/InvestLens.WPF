using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public class ActivityManager : IActivityManager
{
    public List<ActivityItem> GetActivityItems()
    {
        var result = new List<ActivityItem>
        {
            new ActivityItem("🟢", "Покупка AAPL", "+125 акций", "$21,875", "2 дня назад"),
            new ActivityItem("🔴", "Продажа TSLA", "-50 акций", "$8,250", "5 дней назад"),
            new ActivityItem("💵", "Дивиденды MSFT", "Квартальные", "$420", "12 дней назад"),
        };

        return result;
    }
}

public interface IActivityManager
{
    List<ActivityItem> GetActivityItems();
}