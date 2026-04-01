using System.ComponentModel;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class MainWindowViewModel : BindableBase, IMainWindowViewModel
{
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IEventAggregator _eventAggregator;

    public MainWindowViewModel(INavigationViewModel navigationVm, 
        IHeaderViewModel headerVm, 
        IViewModelFactory viewModelFactory, 
        IEventAggregator eventAggregator)
    {
        _viewModelFactory = viewModelFactory;
        _eventAggregator = eventAggregator;
        NavigationVm = navigationVm;
        HeaderVm = headerVm;

        //Remove
        UserAvatar = "АЮ";
        UserName = "Александр Ю.";
        NotificationsCount = new Random().Next(100);

        _eventAggregator.GetEvent<SelectMenuNodeEvent>().Subscribe(OnSelectMenuNode);
    }

    public INavigationViewModel NavigationVm { get; }
    public IHeaderViewModel HeaderVm { get; }
    public INotifyPropertyChanged? ContentVm { get; private set; }

    public int NotificationsCount { get; set; }
    public string NotificationsCountDisplay => NotificationsCount <= 9 ? NotificationsCount.ToString() : "9+";
    public bool HasNotifications => NotificationsCount > 0;
    public string UserAvatar { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    private void OnSelectMenuNode(MenuNode node)
    {
        HeaderVm.SetModel(node);
        ContentVm = GetContentVm(node.NodeType);
    }

    private INotifyPropertyChanged GetContentVm(NodeTypes nodeType)
    {
        return _viewModelFactory.CreateViewModel(nodeType);
    }
}