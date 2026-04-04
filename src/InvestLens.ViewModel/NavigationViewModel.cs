using System.Windows.Input;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class NavigationViewModel : BindableBase, INavigationViewModel
{
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IEventAggregator _eventAggregator;
    
    public NavigationViewModel(IPortfoliosManager portfoliosManager, IEventAggregator eventAggregator)
    {
        _portfoliosManager = portfoliosManager;
        _eventAggregator = eventAggregator;
        MenuItems = GetMenuItems();

        NavigateCommand = new DelegateCommand<MenuNode>(OnNavigate);
    }

    public ICommand NavigateCommand { get; }

    public List<MenuItemModel> MenuItems { get; set; }

    private void OnNavigate(MenuNode node)
    {
        _eventAggregator.GetEvent<SelectMenuNodeEvent>().Publish(node);
    }

    private List<MenuItemModel> GetMenuItems()
    {
        var result = new List<MenuItemModel>
        {
            new MenuNode(NodeTypes.Dashboard, "🏠", "Главная"){Title = "Главная", Description = "Обзор инвестиционной активности"},
            new MenuDivider(),
            new MenuNode(NodeTypes.Portfolios, "📁", "Портфели", _portfoliosManager.GetPortfoliosMenuItems()) {Title = "Портфели", Description = "Управление инвестиционными портфелями"},
            new MenuNode(NodeTypes.Dictionaries, "📚", "Справочники", GetDictionariesMenuItems()){Title = "Справочники", Description = "Источники рыночных данных и справочной информации"},
            new MenuDivider(),
            new MenuNode(NodeTypes.Downloader, "⬇️", "Менеджер закачек"){Title = "Менеджер закачек", Description = "Управление загрузкой данных"},
            new MenuDivider(),
            new MenuNode(NodeTypes.Scheduler, "📅", "Планировщик"){Title = "Планировщик", Description = "Управление задачами и напоминаниями"},
            new MenuDivider(),
            new MenuNode(NodeTypes.Settings, "⚙️", "Настройки", GetSettingsMenuItems()){Title = "Настройки", Description = "Настройка приложения и управление плагинами"}
        };

        return result;
    }

    private List<MenuNode> GetDictionariesMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode(NodeTypes.DictionariesMoex, "🏛️", "MOEX", GetMoexMenuItems()){Title = "Московская Биржа (MOEX)", Description = "Основные рыночные инструменты и индексы"},
            new MenuNode(NodeTypes.DictionariesDohod, "🌐", "Dohod.ru", GetDohodMenuItems()){Title = "Dohod.ru", Description = "Агрегатор данных по облигациям"}
        };

        return result;
    }

    private List<MenuNode> GetMoexMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode(NodeTypes.DictionariesMoexSecurities, "📈", "Ценные бумаги"){Title = "Ценные бумаги (MOEX)", Description = "Акции, ETF и другие инструменты"},
            new MenuNode(NodeTypes.DictionariesMoexBonds, "📜", "Облигации"){Title = "Облигации (MOEX)", Description = "Облигации на Московской бирже"}
        };

        return result;
    }

    private List<MenuNode> GetDohodMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode(NodeTypes.DictionariesDohodBonds, "📜", "Облигации"){Title = "Облигации (Dohod.ru)", Description = "Данные с агрегатора dohod.ru"}
        };

        return result;
    }

    private List<MenuNode> GetSettingsMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode(NodeTypes.SettingsCommon, "🔧", "Общие"){Title = "Общие настройки", Description = "Настройки интерфейса и форматов"},
            new MenuNode(NodeTypes.SettingsPlugins, "🧩", "Плагины"){Title = "Плагины", Description = "Управление расширениями"}
        };

        return result;
    }
}