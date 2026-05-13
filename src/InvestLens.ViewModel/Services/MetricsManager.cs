using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.Events;

namespace InvestLens.ViewModel.Services;

public class MetricsManager : IMetricsManager
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly ITransactionRepository _repository;
    private readonly Dictionary<int, decimal> _currentPortfolioCostCache = [];
    private CancellationTokenSource? _initcanCellationTokenSource;

    public MetricsManager(
        IEventAggregator eventAggregator,
        IPortfoliosManager portfoliosManager,
        ITransactionRepository repository)
    {
        _eventAggregator = eventAggregator;
        _portfoliosManager = portfoliosManager;
        _repository = repository;

        _eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLogin);
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

    // ToDo Вычилсять текущую стоимость при запуске и сохранять в кэше

    public decimal CurrentCost()
    {
        return _currentPortfolioCostCache.Sum(c => c.Value);
    }

    public decimal PortfolioCurrentCost(int[] ids)
    {
        var total = 0m;
        foreach (var id in ids)
        {
            if (_currentPortfolioCostCache.TryGetValue(id, out var cost))
            {
                total += cost;
            }
        }
        return total;
    }

    private async Task<decimal> ComputePortfolioCurrentCost(int[] ids, CancellationToken ct)
    {
        return await _repository.GetPortfolioCurrentCost(ids, ct);
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

    #region Metrics

    public async Task<List<MetricCard>> GetMetricCards(CancellationToken ct)
    {
        var totalCashIn = await TotalCashIn();
        var currentCost = CurrentCost();
        var totalDividends = await TotalDividends();
        // ( (Текущая стоимость − Вложено + Дивиденды) / Вложено ) × 100%
        var totalProfit = totalCashIn > 0
            ? (currentCost - totalCashIn + totalDividends) / totalCashIn
            : 0;

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💸", Label = "Вложено", Value = totalCashIn.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💰", Label = "Стоимость", Value = currentCost.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💵", Label = "Дивиденды", Value = totalDividends.ToString("C2"), Change = "", IsPositive = false },
            new MetricCard { Icon = "📈", Label = "Относительная доходность", Value = totalProfit.ToString("P2"), Change = "", IsPositive = true }
        };

        return result;
    }

    public async Task<List<MetricCard>> GetPortfolioMetricCards(int[] ids)
    {
        var totalCashIn = await PortfolioTotalCashIn(ids);
        var currentCost = PortfolioCurrentCost(ids);
        var totalDividends = await PortfolioTotalDividends(ids);
        // ( (Текущая стоимость − Вложено + Дивиденды) / Вложено ) × 100%
        var totalProfit = totalCashIn > 0 
            ? (currentCost - totalCashIn + totalDividends) / totalCashIn
            : 0;

        var result = new List<MetricCard>
        {
            new MetricCard { Icon = "💸", Label = "Вложено", Value = totalCashIn.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💰", Label = "Стоимость", Value = currentCost.ToString("C2"), Change = "", IsPositive = true },
            new MetricCard { Icon = "💵", Label = "Дивиденды", Value = totalDividends.ToString("C2"), Change = "", IsPositive = false },
            new MetricCard { Icon = "📈", Label = "Относительная доходность", Value = totalProfit.ToString("P2"), Change = "", IsPositive = true }
        };

        return result;
    }

    #endregion Metrics

    #region DynamicMetrics

    public async Task<Dictionary<DateTime, decimal>> DynamicMetrics(PortfolioDynamicPeriod period, CancellationToken ct)
    {
        var result = await _repository.GetDynamicMetrics(period, ct);
        return result;
    }

    public async Task<Dictionary<DateTime, decimal>> PortfolioDynamicMetrics(PortfolioDynamicPeriod period, int[] ids, CancellationToken ct)
    {
        var result = await _repository.GetPortfolioDynamicMetrics(period, ids, ct);
        return result;
    }

    #endregion DynamicMetrics

    private async void OnLogin(UserInfo info)
    {
        _initcanCellationTokenSource?.Cancel();
        _initcanCellationTokenSource = new();

        try
        {
            await Init(_initcanCellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {

        }
        catch
        {
            throw;
        }
    }

    private async Task Init(CancellationToken ct)
    {
        var ids = _portfoliosManager.GetAllPortfolioIds(PortfolioType.Invest);
        foreach (var id in ids)
        {
            if (ct.IsCancellationRequested) return;
            var currentCost = await ComputePortfolioCurrentCost([id], ct);
            _currentPortfolioCostCache[id] = currentCost;
        }
    }
}