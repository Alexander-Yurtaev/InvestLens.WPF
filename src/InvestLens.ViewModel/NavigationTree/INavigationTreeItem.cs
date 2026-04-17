using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.NavigationTree;

public interface INavigationTreeItem
{
    ObservableCollection<INavigationTreeItem> Children { get; set; }
}