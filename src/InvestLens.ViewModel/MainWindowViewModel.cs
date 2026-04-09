using System.ComponentModel;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class MainWindowViewModel : BindableBase, IMainWindowViewModel
{
    private readonly IViewModelFactory _viewModelFactory;
    private INotifyPropertyChanged? _contentVm;

    public MainWindowViewModel(
        INavigationViewModel navigationVm, 
        IHeaderViewModel headerVm,
        IViewModelFactory viewModelFactory, 
        IEventAggregator eventAggregator)
    {
        _viewModelFactory = viewModelFactory;
        NavigationVm = navigationVm;
        HeaderVm = headerVm;

        eventAggregator.GetEvent<SelectMenuNodeEvent>().Subscribe(OnSelectMenuNode);
    }

    public INavigationViewModel NavigationVm { get; }
    public IHeaderViewModel HeaderVm { get; }

    public INotifyPropertyChanged? ContentVm
    {
        get => _contentVm;
        private set => SetProperty(ref _contentVm, value);
    }

    private void OnSelectMenuNode(MenuItemModel node)
    {
        ContentVm = GetContentVm(node.NodeType);
    }

    private INotifyPropertyChanged GetContentVm(NodeType nodeType)
    {
        return _viewModelFactory.CreateViewModel(nodeType);
    }
}