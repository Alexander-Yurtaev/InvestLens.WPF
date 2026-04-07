using InvestLens.ViewModel.Wrappers.Menu;
using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface INavigationViewModel
{
    List<IMenuNode> MenuItems { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
}