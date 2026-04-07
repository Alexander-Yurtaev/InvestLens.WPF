using InvestLens.Model.Enums;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers.Menu;

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

    public List<IMenuNode> MenuItems { get; set; }

    private List<IMenuNode> GetMenuItems()
    {
        var result = new List<IMenuNode>
        {
            new MenuItemWrapper(new MenuItemModel(NodeType.Dashboard, "🏠", "Главная"){Title = "Главная", Description = "Обзор инвестиционной активности"}, _eventAggregator),
            new MenuDivider(),
            new MenuItemWrapper(new MenuItemModel(NodeType.Portfolios, "📁", "Портфели", _portfoliosManager.GetPortfoliosMenuItems()) {Title = "Портфели", Description = "Управление инвестиционными портфелями"}, _eventAggregator),
            new MenuItemWrapper(new MenuItemModel(NodeType.Dictionaries, "📚", "Справочники", GetDictionariesMenuItems()){Title = "Справочники", Description = "Источники рыночных данных и справочной информации"}, _eventAggregator),
            new MenuDivider(),
            new MenuItemWrapper(new MenuItemModel(NodeType.Downloader, "⬇️", "Менеджер закачек"){Title = "Менеджер закачек", Description = "Управление загрузкой данных"}, _eventAggregator),
            new MenuDivider(),
            new MenuItemWrapper(new MenuItemModel(NodeType.Scheduler, "📅", "Планировщик"){Title = "Планировщик", Description = "Управление задачами и напоминаниями"}, _eventAggregator),
            new MenuDivider(),
            new MenuItemWrapper(new MenuItemModel(NodeType.Settings, "⚙️", "Настройки", GetSettingsMenuItems()){Title = "Настройки", Description = "Настройка приложения и управление плагинами"}, _eventAggregator)
        };

        return result;
    }

    private List<MenuItemModel> GetDictionariesMenuItems()
    {
        var result = new List<MenuItemModel>
        {
            new MenuItemModel(NodeType.DictionariesMoex, "🏛️", "MOEX", GetMoexMenuItems()){Title = "Московская Биржа (MOEX)", Description = "Основные рыночные инструменты и индексы"},
            new MenuItemModel(NodeType.DictionariesDohod, "🌐", "Dohod.ru", _dohodService.GetDohodBondsMenuItems()){Title = "Dohod.ru", Description = "Агрегатор данных по облигациям"}
        };

        return result;
    }

    private List<MenuItemModel> GetMoexMenuItems()
    {
        var result = new List<MenuItemModel>
        {
            new MenuItemModel(NodeType.DictionariesMoexSecurities, "📈", "Ценные бумаги"){Title = "Ценные бумаги (MOEX)", Description = "Акции, ETF и другие инструменты"},
            new MenuItemModel(NodeType.DictionariesMoexBonds, "📜", "Облигации"){Title = "Облигации (MOEX)", Description = "Облигации на Московской бирже"}
        };

        return result;
    }

    private List<MenuItemModel> GetSettingsMenuItems()
    {
        var result = new List<MenuItemModel>
        {
            new MenuItemModel(NodeType.SettingsCommon, "🔧", "Общие"){Title = "Общие настройки", Description = "Настройки интерфейса и форматов"},
            new MenuItemModel(NodeType.SettingsPlugins, "🧩", "Плагины"){Title = "Плагины", Description = "Управление расширениями"}
        };

        return result;
    }
}