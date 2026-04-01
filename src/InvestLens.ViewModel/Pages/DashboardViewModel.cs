using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DashboardViewModel : BindableBase, IDashboardViewModel
{
    private readonly IUserManager _userManager;
    private readonly IMetricsManager _metricsManager;

    public DashboardViewModel(IUserManager userManager, IMetricsManager metricsManager)
    {
        _userManager = userManager;
        _metricsManager = metricsManager;
        MetricCards = _metricsManager.GetMetricCards();
    }

    public string WelcomeTitle => $"Добро пожаловать, {_userManager.UserName}";
    public string WelcomeDescription => "Вот что происходит с вашими инвестициями сегодня";

    public List<MetricCard> MetricCards { get; set; }
}