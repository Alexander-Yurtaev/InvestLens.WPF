using InvestLens.Shared.Model.NavigationTree;
using System.ComponentModel;

namespace InvestLens.ViewModel.Services;

public interface IViewModelFactory
{
    Task<INotifyPropertyChanged> CreateViewModel(BaseNavigationTreeModel model);
}