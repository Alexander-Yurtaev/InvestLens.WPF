using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using System.ComponentModel;

namespace InvestLens.ViewModel.Windows;

public class MainWindowViewModel : BindableBase, IMainWindowViewModel
{
    private readonly IViewModelFactory _viewModelFactory;
    private INotifyPropertyChanged? _contentVm;
    private BaseNavigationTreeModel? _currentNavigationTreeModel;
    private bool _isBusy;

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

    public bool IsBusy
    { 
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public INotifyPropertyChanged? ContentVm
    {
        get => _contentVm;
        private set => SetProperty(ref _contentVm, value);
    }

    private async void OnSelectMenuNode(BaseNavigationTreeModel model)
    {
        if (model == _currentNavigationTreeModel)
        {
            return;
        }

        var viewModel = await GetContentVm(model);
        if (viewModel is ILoadableViewModel loadable)
        {
            await loadable.Load();
        }

        ContentVm = viewModel;
        _currentNavigationTreeModel = model;
    }

    private async Task<INotifyPropertyChanged> GetContentVm(BaseNavigationTreeModel model)
    {
        return await _viewModelFactory.CreateViewModel(model);
    }
}