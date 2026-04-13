using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using System.ComponentModel;

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

        eventAggregator.GetEvent<SelectNavigationItemEvent>().Subscribe(OnSelectMenuNode);
    }

    public INavigationViewModel NavigationVm { get; }
    public IHeaderViewModel HeaderVm { get; }

    public INotifyPropertyChanged? ContentVm
    {
        get => _contentVm;
        private set => SetProperty(ref _contentVm, value);
    }

    private void OnSelectMenuNode(BaseNavigationTreeModel model)
    {
        ContentVm = GetContentVm(model);
    }

    private INotifyPropertyChanged GetContentVm(BaseNavigationTreeModel model)
    {
        return _viewModelFactory.CreateViewModel(model);
    }
}