using System.Collections.ObjectModel;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.NavigationTree;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class NavigationViewModel : BindableBase, INavigationViewModel
{
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IDohodService _dohodService;
    private readonly IEventAggregator _eventAggregator;

    public NavigationViewModel(IPortfoliosManager portfoliosManager, IDohodService dohodService, IEventAggregator eventAggregator)
    {
        _portfoliosManager = portfoliosManager;
        _dohodService = dohodService;
        _eventAggregator = eventAggregator;
        MenuItems = GetMenuItems();
    }

    public ObservableCollection<INavigationTreeItem> MenuItems { get; set; }

    private ObservableCollection<INavigationTreeItem> GetMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem("🏠", "Главная", new DashboardNavigationTreeModel{Title = "Главная", Description = "Обзор инвестиционной активности"}, _eventAggregator),
            new NavigationTreeDivider(),
            new NavigationTreeItem("📁", "Портфели", new PortfoliosNavigationTreeModel{Title = "Портфели", Description = "Управление инвестиционными портфелями"}, _eventAggregator, _portfoliosManager.GetPortfoliosMenuItems()),
            new NavigationTreeItem("📚", "Справочники", new DictionariesNavigationTreeModel{Title = "Справочники", Description = "Источники рыночных данных и справочной информации"}, _eventAggregator, GetDictionariesMenuItems()),
            new NavigationTreeDivider(),
            new NavigationTreeItem("⬇️", "Менеджер закачек", new DownloaderNavigationTreeModel{Title = "Менеджер закачек", Description = "Управление загрузкой данных"}, _eventAggregator),
            new NavigationTreeDivider(),
            new NavigationTreeItem("📅", "Планировщик", new SchedulerNavigationTreeModel{Title = "Планировщик", Description = "Управление задачами и напоминаниями"}, _eventAggregator),
            new NavigationTreeDivider(),
            new NavigationTreeItem("⚙️", "Настройки", new SettingsNavigationTreeModel{Title = "Настройки", Description = "Настройка приложения и управление плагинами"}, _eventAggregator, GetSettingsMenuItems())
        };

        return new ObservableCollection<INavigationTreeItem>(result);
    }

    private List<INavigationTreeItem> GetDictionariesMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem("🏛️", "MOEX", new DictionariesMoexNavigationTreeModel{Title = "Московская Биржа (MOEX)", Description = "Основные рыночные инструменты и индексы"}, _eventAggregator, GetMoexMenuItems()),
            new NavigationTreeItem("🌐", "Dohod.ru", new DictionariesDohodNavigationTreeModel{Title = "Dohod.ru", Description = "Агрегатор данных по облигациям"}, _eventAggregator, _dohodService.GetDohodBondsMenuItems())
        };

        return result;
    }

    private List<INavigationTreeItem> GetMoexMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem("📈", "Ценные бумаги", new DictionariesMoexSecuritiesNavigationTreeModel{Title = "Ценные бумаги (MOEX)", Description = "Акции, ETF и другие инструменты"}, _eventAggregator),
            new NavigationTreeItem("📜", "Облигации", new DictionariesMoexBondsNavigationTreeModel{Title = "Облигации (MOEX)", Description = "Облигации на Московской бирже"}, _eventAggregator)
        };

        return result;
    }

    private List<INavigationTreeItem> GetSettingsMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem("🔧", "Общие", new SettingsNavigationTreeModel{Title = "Общие настройки", Description = "Настройки интерфейса и форматов"}, _eventAggregator),
            new NavigationTreeItem("🧩", "Плагины", new SettingsPluginsNavigationTreeModel{Title = "Плагины", Description = "Управление расширениями"}, _eventAggregator)
        };

        return result;
    }
}