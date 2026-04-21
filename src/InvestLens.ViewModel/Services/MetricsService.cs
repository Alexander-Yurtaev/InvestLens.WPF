using InvestLens.DataAccess.Repositories;
using InvestLens.Model;

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

    #region TotalCashIn - сколько вложили (база)

    public async Task<decimal> TotalCashIn()
    {
        return await _repository.GetTotalCashIn();
    }

    public async Task<decimal> PortfolioTotalCashIn(int[] ids)
    {
        return await _repository.GetPortfolioTotalCashIn(ids);
    }

    #endregion TotalCashIn

    #region CurrentCost - текущая стоимость

    public async Task<decimal> CurrentCost()
    {
        return await _repository.GetCurrentCost();
    }

    public async Task<decimal> PortfolioCurrentCost(int[] ids)
    {
        return await _repository.GetPortfolioCurrentCost(ids);
    }

    #endregion CurrentCost

    #region TotalDividends - денежный поток (купоны/дивиденды)

    public async Task<decimal> TotalDividends()
    {
        return await _repository.GetTotalDividends();
    }

    public async Task<decimal> PortfolioTotalDividends(int[] ids)
    {
        return await _repository.GetPortfolioTotalDividends(ids);
    }

    #endregion TotalDividends

    public async Task<List<MetricCard>> GetMetricCards()
    {
        var totalCashIn = await TotalCashIn();
        var currentCost = await CurrentCost();
        var totalDividends = await TotalDividends();
        // ( (Текущая стоимость − Вложено + Дивиденды) / Вложено ) × 100%
        var totalProfit = (currentCost - totalCashIn + totalDividends) / totalCashIn;

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💸", Label = "Вложили", Value = totalCashIn.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💰", Label = "Стоимость", Value = currentCost.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💵", Label = "Дивиденды", Value = totalDividends.ToString("C2"), Change = "", IsPositive = false },
            new MetricCard { Icon = "📈", Label = "Относительная доходность", Value = totalProfit.ToString("P2"), Change = "", IsPositive = true }
        };

        return result;
    }

    public async Task<List<MetricCard>> GetPortfolioMetricCards(int[] ids)
    {
        var totalCashIn = await PortfolioTotalCashIn(ids);
        var currentCost = await PortfolioCurrentCost(ids);
        var totalDividends = await PortfolioTotalDividends(ids);
        // ( (Текущая стоимость − Вложено + Дивиденды) / Вложено ) × 100%
        var totalProfit = (currentCost - totalCashIn + totalDividends) / totalCashIn;

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💸", Label = "Вложили", Value = totalCashIn.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💰", Label = "Стоимость", Value = currentCost.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💵", Label = "Дивиденды", Value = totalDividends.ToString("C2"), Change = "", IsPositive = false },
            new MetricCard { Icon = "📈", Label = "Относительная доходность", Value = totalProfit.ToString("P2"), Change = "", IsPositive = true }
        };

        return result;
    }
}