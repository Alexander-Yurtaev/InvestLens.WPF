using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows;

public interface ICreateUpdatePortfolioWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    string Name { get; set; }
    string Description { get; set; }
    ICommand CloseCommand { get; set; }
}