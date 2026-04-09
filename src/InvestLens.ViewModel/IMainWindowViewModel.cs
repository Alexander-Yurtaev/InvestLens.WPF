using System.ComponentModel;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public interface IMainWindowViewModel
{
    INavigationViewModel NavigationVm { get; }
    IHeaderViewModel HeaderVm { get; }
    INotifyPropertyChanged? ContentVm { get; }

    event PropertyChangedEventHandler? PropertyChanged;
}