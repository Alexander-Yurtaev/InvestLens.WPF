using InvestLens.Model;
using InvestLens.ViewModel.Services;
using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public interface IDashboardViewModel : IViewModelBaseWithContentHeader, ILoadableViewModel
{
    IPortfolioDynamicsViewModel PortfolioDynamicsViewModel { get; }
    ObservableCollection<MetricCard> MetricCards { get; }
    ObservableCollection<ActivityItem> ActivityItems { get; }
}