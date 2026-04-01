using System.ComponentModel;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public interface IMainWindowViewModel
{
    INavigationViewModel NavigationVm { get; }
    IHeaderViewModel HeaderVm { get; }
    INotifyPropertyChanged? ContentVm { get; }

    INotificationsManager NotificationsManager { get; }
    IUserManager UserManager { get; }

    event PropertyChangedEventHandler? PropertyChanged;
}