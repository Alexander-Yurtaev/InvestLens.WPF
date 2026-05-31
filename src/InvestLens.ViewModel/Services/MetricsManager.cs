using InvestLens.Shared.DataAccess.Repositories;
using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Enums;
using InvestLens.ViewModel.Events;

namespace InvestLens.ViewModel.Services;

public class MetricsManager : IMetricsManager
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IWindowManager _windowManager;
    private readonly ITransactionRepository _repository;
    private readonly ISecurityRepository _securityRepository;
    private readonly Dictionary<int, decimal> _currentPortfolioCostCache = [];
    private CancellationTokenSource? _initcanCellationTokenSource;

    public MetricsManager(
        IEventAggregator eventAggregator,
        IPortfoliosManager portfoliosManager,
        IWindowManager windowManager,
        ITransactionRepository repository,
        ISecurityRepository securityRepository)
    {
        _eventAggregator = eventAggregator;
        _portfoliosManager = portfoliosManager;
        _windowManager = windowManager;
        _repository = repository;
        _securityRepository = securityRepository;
        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Subscribe(OnPortfoliosLoaded);
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

    // ToDo Вычислять текущую стоимость при запуске и сохранять в кэше

    public decimal CurrentCost()
    {
        return _currentPortfolioCostCache.Sum(c => c.Value);
    }

    public decimal GetCurrentPortfolioCost(int[] ids)
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

    private async Task<decimal> ComputePortfolioCurrentCost(int id, CancellationToken ct)
    {
        var securities = await _securityRepository.GetLoadedSecurityListAsync(ct);
        return await _repository.GetPortfolioCurrentCost(id, securities, ct);
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
        var currentCost = GetCurrentPortfolioCost(ids);
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

    private async void OnPortfoliosLoaded()
    {
        _initcanCellationTokenSource?.Cancel();
        _initcanCellationTokenSource = new();

        _windowManager.ShowIsBusy("Метрики: обновление...");

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
        finally
        {
            _windowManager.HideIsBusy();
        }
    }

    private async Task Init(CancellationToken ct)
    {
        var ids = _portfoliosManager.GetAllPortfolioIds(PortfolioType.Invest);
        foreach (var id in ids)
        {
            if (ct.IsCancellationRequested) return;
            _windowManager.ShowIsBusy("Вычисление: стоимость...");
            var currentCost = await ComputePortfolioCurrentCost(id, ct);
            _currentPortfolioCostCache[id] = currentCost;
        }

        _eventAggregator.GetEvent<MetricsManagerInitEvent>().Publish();
    }
}