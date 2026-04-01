using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IMetricsManager
{
    List<MetricCard> GetMetricCards();
}