namespace InvestLens.ViewModel;

public class MainWindowViewModel : BindableBase, IMainWindowViewModel
{
    public MainWindowViewModel()
    {
        
    }

    public MainWindowViewModel(INavigationViewModel navigationVm, IHeaderViewModel headerVm)
    {
        NavigationVm = navigationVm;
        HeaderVm = headerVm;
    }

    public INavigationViewModel NavigationVm { get; }
    public IHeaderViewModel HeaderVm { get; }

    public int NotificationsCount { get; set; }
    public string NotificationsCountDisplay => NotificationsCount <= 9 ? NotificationsCount.ToString() : "9+";
    public bool HasNotifications => NotificationsCount > 0;
    public string UserAvatar { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}