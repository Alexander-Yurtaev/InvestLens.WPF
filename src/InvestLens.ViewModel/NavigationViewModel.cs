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

        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Subscribe(OnPortfoliosLoaded);
        _eventAggregator.GetEvent<PortfolioCreatedEvent>().Subscribe(OnPortfolioCreated);
        _eventAggregator.GetEvent<PortfolioUpdatedEvent>().Subscribe(OnPortfolioUpdated);

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

    private void OnPortfoliosLoaded()
    {
        RefreshPortfolioList();
    }

    private void OnPortfolioCreated(int id)
    {
        var portfolio = _portfoliosManager
            .GetPortfoliosMenuItems(_authManager.CurrentUser!.Id)
            .Cast<NavigationTreeItem>()
            .FirstOrDefault(p => (p.Model as PortfolioNavigationTreeModel)?.Id == id);

        if (portfolio is null) return;
        _portfoliosTreeItem!.Children.Add(portfolio);
    }

    private void OnPortfolioUpdated(int id)
    {
        var updatedPortfolio = _portfoliosManager
            .GetPortfoliosMenuItems(_authManager.CurrentUser!.Id)
            .Cast<NavigationTreeItem>()
            .FirstOrDefault(p => (p.Model as PortfolioNavigationTreeModel)?.Id == id);

        if (updatedPortfolio is null) return;

        var currentPortfolio = _portfoliosTreeItem!.Children
            .Cast<NavigationTreeItem>()
            .FirstOrDefault(p => (p.Model as PortfolioNavigationTreeModel)?.Id == id);
        
        if (currentPortfolio is null) return;

        var index = _portfoliosTreeItem!.Children.IndexOf(currentPortfolio);

        _portfoliosTreeItem!.Children[index] = updatedPortfolio;
        _eventAggregator.GetEvent<SelectNavigationItemEvent>().Publish(updatedPortfolio.Model);
    }

    private void RefreshPortfolioList()
    {
        var portfolios = _portfoliosManager
            .GetPortfoliosMenuItems(_authManager.CurrentUser!.Id)
            .Cast<NavigationTreeItem>()
            .ToList();
        
        // Update
        foreach (NavigationTreeItem portfolio in portfolios)
        {
            var portfolioId = ((PortfolioNavigationTreeModel)portfolio.Model).Id;
            var existPortfolio = _portfoliosTreeItem!
                .Children
                .Cast<NavigationTreeItem>()
                .FirstOrDefault(item => ((PortfolioNavigationTreeModel)item.Model).Id == portfolioId);

            if (existPortfolio is not null)
            {
                var index = _portfoliosTreeItem!.Children.IndexOf(existPortfolio);
                _portfoliosTreeItem!.Children[index] = portfolio;
            }
            else
            {
                _portfoliosTreeItem!.Children.Add(portfolio);
            }   
        }

        // Delete
        foreach (NavigationTreeItem portfolio in _portfoliosTreeItem!.Children.ToList())
        {
            var portfolioId = ((PortfolioNavigationTreeModel)portfolio.Model).Id;
            var isExistPortfolio = portfolios.Any(p => ((PortfolioNavigationTreeModel)p.Model).Id == portfolioId);

            if (isExistPortfolio) continue;

            _portfoliosTreeItem!.Children.Remove(portfolio);
        }
    }
}