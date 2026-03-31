using System.Windows.Input;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;

namespace InvestLens.ViewModel;

public class NavigationViewModel : BindableBase, INavigationViewModel
{
    private readonly IEventAggregator _eventAggregator;
    
    public NavigationViewModel()
    {
        _eventAggregator = new EventAggregator();
    }

    public NavigationViewModel(IEventAggregator eventAggregator)
    {
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
            new MenuNode("🏠", "Главная"){Title = "Главная", Description = "Обзор инвестиционной активности"},
            new MenuDivider(),
            new MenuNode("📁", "Портфели", GetPortfoliosMenuItems()) {Title = "Портфели", Description = "Управление инвестиционными портфелями"},
            new MenuNode("📚", "Справочники", GetDictionariesMenuItems()),
            new MenuDivider(),
            new MenuNode("⬇️", "Менеджер закачек"),
            new MenuDivider(),
            new MenuNode("📅", "Планировщик"),
            new MenuDivider(),
            new MenuNode("⚙️", "Настройки", GetSettingsMenuItems())
        };

        return result;
    }

    private List<MenuNode> GetPortfoliosMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("📊", "Составной инвестиционный"){Title = "Составной инвестиционный", Description = "Детальная информация о портфеле"},
            new MenuNode("💰", "Портфель №1"){Title = "Портфель №1", Description = "Детальная информация о портфеле"},
            new MenuNode("💎", "Портфель №2"){Title = "Портфель №2", Description = "Детальная информация о портфеле"}
        };

        return result;
    }

    private List<MenuNode> GetDictionariesMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("🏛️", "MOEX", GetMoexMenuItems()),
            new MenuNode("🌐", "Dohod.ru", GetDohodMenuItems())
        };

        return result;
    }

    private List<MenuNode> GetMoexMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("📈", "Ценные бумаги"){Title = "Ценные бумаги (MOEX)", Description = "Акции, ETF и другие инструменты"},
            new MenuNode("📜", "Облигации"){Title = "Облигации (MOEX)", Description = "Облигации на Московской бирже"}
        };

        return result;
    }

    private List<MenuNode> GetDohodMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("📜", "Облигации"){Title = "Облигации (Dohod.ru)", Description = "Данные с агрегатора dohod.ru"}
        };

        return result;
    }

    private List<MenuNode> GetSettingsMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("🔧", "Общие"){Title = "Общие настройки", Description = "Настройки интерфейса и форматов"},
            new MenuNode("🧩", "Плагины"){Title = "Плагины", Description = "Управление расширениями"}
        };

        return result;
    }
}