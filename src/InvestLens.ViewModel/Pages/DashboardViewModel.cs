using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Services;
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
    private IPortfolioDynamicsViewModel _portfolioDynamicsViewModel;
    private CancellationTokenSource? _portfoliosLoadedCancellationSource;

    public DashboardViewModel(
        IAuthManager authManager,
        IWindowManager windowManager,
        IMetricsManager metricsManager,
        IActivityManager activityManager,
        IPortfolioDynamicsViewModel portfolioDynamicsViewModel,
        IEventAggregator eventAggregator) : base($"Добро пожаловать, Гость",
        "Вот что происходит с вашими инвестициями сегодня")
    {
        eventAggregator.GetEvent<MetricsManagerInitEvent>().Subscribe(OnMetricsManagerInit);
        _authManager = authManager;
        _windowManager = windowManager;
        _metricsManager = metricsManager;
        _activityManager = activityManager;
        _portfolioDynamicsViewModel = portfolioDynamicsViewModel;
        MetricCards = [];
        ActivityItems = [];
    }

    public IPortfolioDynamicsViewModel PortfolioDynamicsViewModel 
    { 
        get => _portfolioDynamicsViewModel; 
        set => SetProperty(ref _portfolioDynamicsViewModel, value); 
    }

    public ObservableCollection<MetricCard> MetricCards { get; }
    public ObservableCollection<ActivityItem> ActivityItems { get; }

    public async Task Load(bool? force = false)
    {
        OnMetricsManagerInit();
        await Task.Delay(0);
    }

    private async void OnMetricsManagerInit()
    {
        _windowManager.ShowIsBusy("Dashboard: инициализация");

        try
        {
            if (_authManager.CurrentUser is null)
            {
                return;
            }

            _portfoliosLoadedCancellationSource?.Cancel();
            _portfoliosLoadedCancellationSource = new();
            var ct = _portfoliosLoadedCancellationSource.Token;

            ContentHeaderVm.SetWelcomeTitle($"Добро пожаловать, {_authManager.CurrentUser.UserName}");

            var metrics = await _metricsManager.GetMetricCards(ct);
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
        finally
        {
            _windowManager.HideIsBusy();
        }
    }
}