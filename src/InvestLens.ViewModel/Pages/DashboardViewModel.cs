using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DashboardViewModel : BindableBase, IDashboardViewModel
{
    private readonly IUserManager _userManager;
    private readonly IMetricsManager _metricsManager;
    private readonly IActivityManager _activityManager;

    public DashboardViewModel(IUserManager userManager, 
        IMetricsManager metricsManager, 
        IActivityManager activityManager,
        IPortfolioDynamicsService portfolioDynamicsService)
    {
        PortfolioDynamicsService = portfolioDynamicsService;
        _userManager = userManager;
        _metricsManager = metricsManager;
        _activityManager = activityManager;
        MetricCards = _metricsManager.GetMetricCards();
        ActivityItems = _activityManager.GetActivityItems();
    }

    public string WelcomeTitle => $"Добро пожаловать, {_userManager.UserName}";
    public string WelcomeDescription => "Вот что происходит с вашими инвестициями сегодня";
    public IPortfolioDynamicsService PortfolioDynamicsService { get; }

    public List<MetricCard> MetricCards { get; }
    public List<ActivityItem> ActivityItems { get; }
}