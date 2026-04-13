using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.NavigationTree;

public abstract class BaseNavigationTreeItem : BindableBase, INavigationTreeItem
{
    public ObservableCollection<INavigationTreeItem> Children { get; set; } = [];
}