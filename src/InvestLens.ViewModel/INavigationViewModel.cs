using System.ComponentModel;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel;

public interface INavigationViewModel
{
    List<MenuItemModel> MenuItems { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
}