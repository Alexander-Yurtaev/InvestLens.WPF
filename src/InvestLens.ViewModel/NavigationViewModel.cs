using System.Collections.ObjectModel;
using InvestLens.Model;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.NavigationTree;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class NavigationViewModel : BindableBase, INavigationViewModel
{
    private readonly IAuthManager _authManager;
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IDohodService _dohodService;
    private readonly IEventAggregator _eventAggregator;
    private NavigationTreeItem? _portfoliosTreeItem;

    public NavigationViewModel(
        IAuthManager authManager,
        IPortfoliosManager portfoliosManager, 
        IDohodService dohodService, 
        IEventAggregator eventAggregator)
    {
        _authManager = authManager;
        _portfoliosManager = portfoliosManager;
        _dohodService = dohodService;
        _eventAggregator = eventAggregator;

        _eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLogin);
        _eventAggregator.GetEvent<PortfolioCreatedEvent>().Subscribe(OnPortfolioCreated);
        
        MenuItems = [];
    }

    public ObservableCollection<INavigationTreeItem> MenuItems { get; set; }

    public async Task LoadAsync()
    {
        var menuItems = await GetMenuItems();

        MenuItems.Clear();
        foreach (var menuItem in menuItems)
        {
            MenuItems.Add(menuItem);
        }
    }
    
    private async Task<List<INavigationTreeItem>> GetMenuItems()
    {
        _portfoliosTreeItem = new NavigationTreeItem(new PortfoliosNavigationTreeModel(), _eventAggregator);

        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem(new DashboardNavigationTreeModel(), _eventAggregator),
            new NavigationTreeDivider(),
            _portfoliosTreeItem,
            new NavigationTreeItem(new DictionariesNavigationTreeModel(), _eventAggregator, GetDictionariesMenuItems()),
            new NavigationTreeDivider(),
            new NavigationTreeItem(new DownloaderNavigationTreeModel(), _eventAggregator),
            new NavigationTreeDivider(),
            new NavigationTreeItem(new SchedulerNavigationTreeModel(), _eventAggregator),
            new NavigationTreeDivider(),
            new NavigationTreeItem(new SettingsNavigationTreeModel(), _eventAggregator, GetSettingsMenuItems())
        };

        return await Task.FromResult(result);
    }

    private List<INavigationTreeItem> GetDictionariesMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem(new DictionariesMoexNavigationTreeModel(), _eventAggregator, GetMoexMenuItems()),
            new NavigationTreeItem(new DictionariesDohodNavigationTreeModel(), _eventAggregator, _dohodService.GetDohodBondsMenuItems())
        };

        return result;
    }

    private List<INavigationTreeItem> GetMoexMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem(new DictionariesMoexSecuritiesNavigationTreeModel(), _eventAggregator),
            new NavigationTreeItem(new DictionariesMoexBondsNavigationTreeModel(), _eventAggregator)
        };

        return result;
    }

    private List<INavigationTreeItem> GetSettingsMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem(new SettingsNavigationTreeModel(), _eventAggregator),
            new NavigationTreeItem(new SettingsPluginsNavigationTreeModel(), _eventAggregator)
        };

        return result;
    }

    private async void OnLogin(UserInfo info)
    {
        await RefreshPortfolioList();
    }

    private async void OnPortfolioCreated()
    {
        await RefreshPortfolioList();
    }

    private async Task RefreshPortfolioList()
    {
        var portfolios = await _portfoliosManager.GetPortfoliosMenuItems(_authManager.CurrentUser!.Id);

        foreach (NavigationTreeItem portfolio in portfolios.Cast<NavigationTreeItem>())
        {
            var portfolioId = ((PortfolioNavigationTreeModel)portfolio.Model).Id;
            var existPortfolio = _portfoliosTreeItem?
                .Children
                .Cast<NavigationTreeItem>()
                .FirstOrDefault(item => ((PortfolioNavigationTreeModel)item.Model).Id == portfolioId);

            if (existPortfolio is not null) continue;

            _portfoliosTreeItem?.Children.Add(portfolio);
        }
    }
}