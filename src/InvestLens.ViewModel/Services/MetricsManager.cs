using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public class MetricsManager : IMetricsManager
{
    public List<MetricCard> GetMetricCards()
    {
        var result = new List<MetricCard>
        {
            new MetricCard{Icon = "💰", Label = "Общая стоимость портфеля", Value = "$124,532", Change = "▲ +12.4% за месяц", IsPositive = true},
            new MetricCard{Icon = "📈", Label = "Доходность YTD", Value = "+18.2%", Change = "▲ +5.2% к кварталу", IsPositive = true},
            new MetricCard{Icon = "💸", Label = "Дивиденды (годовые)", Value = "$3,240", Change = "Див. доходность 2.6%", IsPositive = false},
            new MetricCard{Icon = "⚠️", Label = "Риск портфеля (β)", Value = "0.86", Change = "Ниже рынка", IsPositive = true},
        };

        return result;
    }
}