using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IMetricsService
{
    Task<List<MetricCard>> GetMetricCards();
    Task<List<MetricCard>> GetPortfolioMetricCards(int[] ids);

    // сколько вложили(база)
    Task<decimal> TotalCashIn();
    Task<decimal> PortfolioTotalCashIn(int[] ids);

    // текущая стоимость
    Task<decimal> CurrentCost();
    Task<decimal> PortfolioCurrentCost(int[] ids);

    // денежный поток(купоны/дивиденды)
    Task<decimal> TotalDividends();
    Task<decimal> PortfolioTotalDividends(int[] ids);
}