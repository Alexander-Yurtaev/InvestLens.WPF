namespace InvestLens.Model.Menu;

public abstract class MenuItemWithHeaderModel(string icon, string header) : MenuItemModel
{
    public string Icon { get; } = icon;
    public string Header { get; } = header;
}