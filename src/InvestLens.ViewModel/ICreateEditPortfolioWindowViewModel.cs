using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface ICreateEditPortfolioWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    string Name { get; set; }
    string Description { get; set; }
    ICommand CloseCommand { get; set; }
}