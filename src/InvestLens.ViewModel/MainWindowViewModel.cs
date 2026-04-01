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
        IUserManager userManager,
        INotificationsManager notificationsManager,
        IViewModelFactory viewModelFactory, 
        IEventAggregator eventAggregator)
    {
        _viewModelFactory = viewModelFactory;
        _eventAggregator = eventAggregator;
        NavigationVm = navigationVm;
        HeaderVm = headerVm;

        UserManager = userManager;
        NotificationsManager = notificationsManager;

        _eventAggregator.GetEvent<SelectMenuNodeEvent>().Subscribe(OnSelectMenuNode);
    }

    public INavigationViewModel NavigationVm { get; }
    public IHeaderViewModel HeaderVm { get; }
    public INotifyPropertyChanged? ContentVm { get; private set; }

    public INotificationsManager NotificationsManager { get; }
    public IUserManager UserManager { get; }

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