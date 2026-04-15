namespace InvestLens.Model.NavigationTree;

public abstract class BaseNavigationTreeModel (string icon, string title)
{
    public string Icon { get; set; } = icon;
    public string Title { get; set; } = title;
    public string Description { get; set; } = string.Empty;
}