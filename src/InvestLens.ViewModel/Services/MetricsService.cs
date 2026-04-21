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
        return await _repository.GetTotalCost();
    }

    public async Task<decimal> PortfolioTotalCost(int id)
    {
        return await _repository.GetPortfolioTotalCost(id);
    }

    #endregion TotalCost

    #region Yield - относительная доходность (%)

    public async Task<decimal> Yield()
    {
        return await _repository.GetYield();
    }

    public async Task<decimal> PortfolioYield(int id)
    {
        return await _repository.GetPortfolioYield(id);
    }

    #endregion Yield

    #region Dividends - денежный поток (купоны/дивиденды)

    public async Task<decimal> Dividends()
    {
        return await _repository.GetDividends();
    }

    public async Task<decimal> PortfolioDividends(int id)
    {
        return await _repository.GetPortfolioDividends(id);
    }

    #endregion Dividends

    #region YTD - абсолютный финансовый результат (₽/$/€)

    public async Task<decimal> ProfitYTD()
    {
        return await _repository.GetProfitYTD();
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
        var totalDividend = await Dividends();
        var profitYTD = await ProfitYTD();

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💰", Label = "Сколько вложили", Value = totalCost.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "📈", Label = "Относительная доходность", Value = TotalYield.ToString("P2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💸", Label = "Дивиденды", Value = totalDividend.ToString("C2"), Change = "", IsPositive = false },
            new MetricCard { Icon = "🎯", Label = "Абсолютный финансовый результат", Value = profitYTD.ToString("C2"), Change = "", IsPositive = true }
        };

        return result;
    }
}