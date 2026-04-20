using InvestLens.Model;
using InvestLens.Model.Services;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public class DashboardViewModel : ViewModelBaseWithContentHeader, IDashboardViewModel
{
    private readonly IAuthManager _authManager;
    private readonly IWindowManager _windowManager;
    private readonly IMetricsManager _metricsManager;
    private readonly IActivityManager _activityManager;

    public DashboardViewModel(
        IAuthManager authManager,
        IWindowManager windowManager,
        IMetricsManager metricsManager,
        IActivityManager activityManager,
        IPortfolioDynamicsService portfolioDynamicsService,
        IEventAggregator eventAggregator) : base($"Добро пожаловать, Гость",
        "Вот что происходит с вашими инвестициями сегодня")
    {
        eventAggregator.GetEvent<PortfoliosLoadedEvent>().Subscribe(OnPortfoliosLoaded);
        _authManager = authManager;
        _windowManager = windowManager;
        _metricsManager = metricsManager;
        _activityManager = activityManager;
        PortfolioDynamicsService = portfolioDynamicsService;
        MetricCards = [];
        ActivityItems = [];
    }

    public IPortfolioDynamicsService PortfolioDynamicsService { get; }

    public ObservableCollection<MetricCard> MetricCards { get; }
    public ObservableCollection<ActivityItem> ActivityItems { get; }

    public async Task Load(bool? force = false)
    {
        OnPortfoliosLoaded();
    }

    private async void OnPortfoliosLoaded()
    {
        if (_authManager.CurrentUser is null)
        {
            return;
        }

        ContentHeaderVm.SetWelcomeTitle($"Добро пожаловать, {_authManager.CurrentUser.UserName}");
        
        var metrics = await _metricsManager.GetMetricCards();
        MetricCards.Clear();
        foreach (var metric in metrics)
        {
            MetricCards.Add(metric);
        }

        var activityItems = await _activityManager.GetActivityItems();
        ActivityItems.Clear();
        foreach (var activityItem in activityItems)
        {
            ActivityItems.Add(activityItem);
        }
    }
}