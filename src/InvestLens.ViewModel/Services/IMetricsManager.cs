using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IMetricsManager
{
    Task<List<MetricCard>> GetMetricCards();
}