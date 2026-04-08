using InvestLens.Model;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : IViewModelBaseWithContentHeader
{
    List<MetricCard> MetricCards { get; }
    List<ActivityItem> ActivityItems { get; }
}