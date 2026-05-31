using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Enums;

namespace InvestLens.ViewModel.Services;

public interface IMetricsManager
{
    Task<List<MetricCard>> GetMetricCards(CancellationToken ct);
    Task<List<MetricCard>> GetPortfolioMetricCards(int[] ids);

    // сколько вложили(база)
    Task<decimal> TotalCashIn();
    Task<decimal> PortfolioTotalCashIn(int[] ids);

    // текущая стоимость
    decimal CurrentCost();
    decimal GetCurrentPortfolioCost(int[] ids);

    // денежный поток(купоны/дивиденды)
    Task<decimal> TotalDividends();
    Task<decimal> PortfolioTotalDividends(int[] ids);

    // DynamicMetrics
    Task<Dictionary<DateTime, decimal>> DynamicMetrics(PortfolioDynamicPeriod period, CancellationToken ct);
    Task<Dictionary<DateTime, decimal>> PortfolioDynamicMetrics(PortfolioDynamicPeriod period, int[] ids, CancellationToken ct);
}