using InvestLens.Model.NavigationTree;
using System.ComponentModel;

namespace InvestLens.ViewModel.Services;

public interface IViewModelFactory
{
    INotifyPropertyChanged CreateViewModel(BaseNavigationTreeModel model);
}