using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IMetricsService
{
    Task<List<MetricCard>> GetMetricCards();

    // сколько вложили(база)
    Task<decimal> TotalCost();
    Task<decimal> PortfolioTotalCost(int id);

    // относительная доходность(%)
    Task<decimal> Yield();
    Task<decimal> PortfolioYield(int id);

    // денежный поток(купоны/дивиденды)
    Task<decimal> Dividends();
    Task<decimal> PortfolioDividends(int id);

    //абсолютный финансовый результат(₽/$/€)
    Task<decimal> ProfitYTD();
    Task<decimal> PortfolioProfitYTD(int id);
}