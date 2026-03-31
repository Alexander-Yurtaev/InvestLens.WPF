using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface IMainWindowViewModel
{
    INavigationViewModel NavigationVm { get; }
    IHeaderViewModel HeaderVm { get; }

    int NotificationsCount { get; set; }
    bool HasNotifications { get; }
    string UserAvatar { get; set; }
    string UserName { get; set; }

    event PropertyChangedEventHandler? PropertyChanged;
}