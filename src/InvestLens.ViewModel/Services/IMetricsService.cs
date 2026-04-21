using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IMetricsService
{
    Task<List<MetricCard>> GetMetricCards();
}