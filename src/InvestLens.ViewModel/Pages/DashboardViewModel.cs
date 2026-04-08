using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DashboardViewModel : ViewModelBaseWithContentHeader, IDashboardViewModel
{
    public DashboardViewModel(IUserManager userManager,
        IMetricsManager metricsManager,
        IActivityManager activityManager,
        IPortfolioDynamicsService portfolioDynamicsService) : base($"Добро пожаловать, {userManager.UserName}",
        "Вот что происходит с вашими инвестициями сегодня")
    {
        PortfolioDynamicsService = portfolioDynamicsService;
        MetricCards = metricsManager.GetMetricCards();
        ActivityItems = activityManager.GetActivityItems();
    }

    public IPortfolioDynamicsService PortfolioDynamicsService { get; }

    public List<MetricCard> MetricCards { get; }
    public List<ActivityItem> ActivityItems { get; }
}