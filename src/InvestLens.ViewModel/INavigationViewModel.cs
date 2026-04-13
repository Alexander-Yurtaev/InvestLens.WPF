using System.Collections.ObjectModel;
using InvestLens.ViewModel.NavigationTree;
using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface INavigationViewModel : INotifyPropertyChanged
{
    ObservableCollection<INavigationTreeItem> MenuItems { get; set; }
}