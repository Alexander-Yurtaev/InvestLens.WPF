using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;

namespace InvestLens.ViewModel;

public class MainWindowViewModel : BindableBase, IMainWindowViewModel
{
    private readonly IEventAggregator _eventAggregator;

    public MainWindowViewModel()
    {
        _eventAggregator = new EventAggregator();
    }

    public MainWindowViewModel(INavigationViewModel navigationVm, IHeaderViewModel headerVm, IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
        NavigationVm = navigationVm;
        HeaderVm = headerVm;

        _eventAggregator.GetEvent<SelectMenuNodeEvent>().Subscribe(OnSelectMenuNode);
    }

    public INavigationViewModel NavigationVm { get; }
    public IHeaderViewModel HeaderVm { get; }

    public int NotificationsCount { get; set; }
    public string NotificationsCountDisplay => NotificationsCount <= 9 ? NotificationsCount.ToString() : "9+";
    public bool HasNotifications => NotificationsCount > 0;
    public string UserAvatar { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    private void OnSelectMenuNode(MenuNode node)
    {
        HeaderVm.Title = node.Title;
        HeaderVm.Description = node.Description;
    }
}