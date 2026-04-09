using InvestLens.Model;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DashboardViewModel : ViewModelBaseWithContentHeader, IDashboardViewModel
{
    public DashboardViewModel(
        IMetricsManager metricsManager,
        IActivityManager activityManager,
        IPortfolioDynamicsService portfolioDynamicsService,
        IEventAggregator eventAggregator) : base($"Добро пожаловать, Гость",
        "Вот что происходит с вашими инвестициями сегодня")
    {
        eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLogin);
        PortfolioDynamicsService = portfolioDynamicsService;
        MetricCards = metricsManager.GetMetricCards();
        ActivityItems = activityManager.GetActivityItems();
    }

    public IPortfolioDynamicsService PortfolioDynamicsService { get; }

    public List<MetricCard> MetricCards { get; }
    public List<ActivityItem> ActivityItems { get; }

    private void OnLogin(UserInfo userInfo)
    {
        ContentHeaderVm.SetWelcomeTitle($"Добро пожаловать, {userInfo.UserName}");
    }
}