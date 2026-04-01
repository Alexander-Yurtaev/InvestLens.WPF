using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DashboardViewModel : BindableBase, IDashboardViewModel
{
    private readonly IUserManager _userManager;

    public DashboardViewModel(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public string WelcomeTitle => $"Добро пожаловать, {_userManager.UserName}";
    public string WelcomeDescription => "Вот что происходит с вашими инвестициями сегодня";
}