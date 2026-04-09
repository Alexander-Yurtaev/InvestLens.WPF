using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : IViewModelBaseWithContentHeader
{
    IPortfolioDynamicsService PortfolioDynamicsService { get; }
    List<MetricCard> MetricCards { get; }
    List<ActivityItem> ActivityItems { get; }
}