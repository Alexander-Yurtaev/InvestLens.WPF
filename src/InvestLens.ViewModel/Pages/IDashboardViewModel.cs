using InvestLens.Model;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : IBaseViewModel
{
    List<MetricCard> MetricCards { get; }
    List<ActivityItem> ActivityItems { get; }
}