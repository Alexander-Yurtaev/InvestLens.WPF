using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class MetricsService : IMetricsService
{
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly ITransactionRepository _repository;

    public MetricsService(
        IPortfoliosManager portfoliosManager,
        ITransactionRepository repository)
    {
        _portfoliosManager = portfoliosManager;
        _repository = repository;
    }

    #region TotalCost - сколько вложили (база)

    public async Task<decimal> TotalCost()
    {
        return await _repository.GetTotalCostAsync();
    }

    public Task<decimal> PortfolioTotalCost(int id)
    {
        throw new NotImplementedException();
    }

    #endregion TotalCost

    #region Yield - относительная доходность (%)

    public async Task<decimal> Yield()
    {
        return await _repository.GetYield();
    }

    public Task<decimal> PortfolioYield(int id)
    {
        throw new NotImplementedException();
    }

    #endregion Yield

    #region Dividends - денежный поток (купоны/дивиденды)

    public Task<decimal> Dividends()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> PortfolioDividends(int id)
    {
        throw new NotImplementedException();
    }

    #endregion Dividends

    #region YTD - абсолютный финансовый результат (₽/$/€)

    public Task<decimal> ProfitYTD()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> PortfolioProfitYTD(int id)
    {
        throw new NotImplementedException();
    }

    #endregion YTD

    public async Task<List<MetricCard>> GetMetricCards()
    {
        var totalCost = await TotalCost();
        var TotalYield = await Yield();
        var dividend = 0m;
        var risk = 0m;

        var portfolios = _portfoliosManager.GetAllPortfolios(PortfolioType.Invest);
        foreach (var portfolio in portfolios)
        {
            var detailes = await _portfoliosManager.GetPortfolioDetiails(portfolio.Id);
            dividend += detailes?.PortfolioStats.First(s => s.Title == Stat.DividendStat).Value ?? 0m;
        }

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💰", Label = "Сколько вложили", Value = totalCost.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "📈", Label = "Относительная доходность", Value = Yield.ToString("P"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💸", Label = "Дивиденды", Value = dividend.ToString(), Change = "", IsPositive = false },
            new MetricCard { Icon = "⚠️", Label = "Абсолютный финансовый результат", Value = risk.ToString(), Change = "", IsPositive = true }
        };

        return result;
    }
}