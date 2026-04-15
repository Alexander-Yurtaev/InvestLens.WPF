namespace InvestLens.Model.NavigationTree;

public class SettingsNavigationTreeModel : BaseNavigationTreeModel
{
    public SettingsNavigationTreeModel() : base("⚙️", "Настройки")
    {
        Description = "Настройка приложения и управление плагинами";
    }
}