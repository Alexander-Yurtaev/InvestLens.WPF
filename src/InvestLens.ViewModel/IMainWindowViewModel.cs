using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface IMainWindowViewModel
{
    INavigationViewModel NavigationVm { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
}