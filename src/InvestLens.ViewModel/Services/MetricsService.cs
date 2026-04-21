using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class MetricsService : IMetricsService
{
    private readonly IPortfoliosManager _portfoliosManager;

    public MetricsService(IPortfoliosManager portfoliosManager)
    {
        _portfoliosManager = portfoliosManager;
    }

    public async Task<List<MetricCard>> GetMetricCards()
    {
        var totalCost = 0m;
        var profit = 0m;
        var dividend = 0m;
        var risk = 0m;

        var portfolios = _portfoliosManager.GetAllPortfolios(PortfolioType.Invest);
        foreach (var portfolio in portfolios)
        {
            var detailes = await _portfoliosManager.GetPortfolioDetiails(portfolio.Id);
            totalCost += detailes?.PortfolioStats.First(s => s.Title == Stat.PriceStat).Value ?? 0m;
            dividend += detailes?.PortfolioStats.First(s => s.Title == Stat.DividendStat).Value ?? 0m;
        }

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💰", Label = "Общая стоимость портфеля", Value = totalCost.ToString(), Change = "", IsPositive = true },
            new MetricCard { Icon = "📈", Label = "Доходность YTD", Value = profit.ToString("P"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💸", Label = "Дивиденды (итоговые)", Value = dividend.ToString(), Change = "", IsPositive = false },
            new MetricCard { Icon = "⚠️", Label = "Риск портфеля (β)", Value = risk.ToString(), Change = "", IsPositive = true }
        };

        return result;
    }
}