using InvestLens.Model.Menu;

namespace InvestLens.ViewModel;

public class NavigationViewModel : BindableBase, INavigationViewModel
{
    public NavigationViewModel()
    {
        MenuItems = GetMenuItems();
    }

    public List<MenuItemModel> MenuItems { get; set; }

    private List<MenuItemModel> GetMenuItems()
    {
        var result = new List<MenuItemModel>
        {
            new MenuNode("🏠", "Главная"),
            new MenuDivider(),
            new MenuNode("📁", "Портфели", GetPortfoliosMenuItems()),
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
            new MenuNode("📊", "Составной инвестиционный"),
            new MenuNode("💰", "Портфель №1"),
            new MenuNode("💎", "Портфель №2")
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
            new MenuNode("📈", "Ценные бумаги"),
            new MenuNode("📜", "Облигации")
        };

        return result;
    }

    private List<MenuNode> GetDohodMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("📜", "Облигации")
        };

        return result;
    }

    private List<MenuNode> GetSettingsMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode("🔧", "Общие"),
            new MenuNode("🧩", "Плагины")
        };

        return result;
    }
}